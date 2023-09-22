using Vehman2.Shared;
using Vehman2.Vehicles;
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Vehman2.Permissions;
using Vehman2.Transactions;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;
using Vehman2.Shared;
using Vehman2.Owners;
using Vehman2.Companies;

namespace Vehman2.Transactions
{
    [RemoteService(IsEnabled = false)]
    [Authorize(Vehman2Permissions.Transactions.Default)]
    public class TransactionsAppService : ApplicationService, ITransactionsAppService
    {
        private readonly IDistributedCache<TransactionExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        private readonly ITransactionRepository _transactionRepository;
        private readonly TransactionManager _transactionManager;
        private readonly IRepository<Vehicle, Guid> _vehicleRepository;
        private readonly IOwnerRepository _ownerRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IVehicleRepository _vehicleRepository2;

        public TransactionsAppService(ITransactionRepository transactionRepository, TransactionManager transactionManager, IDistributedCache<TransactionExcelDownloadTokenCacheItem, string> excelDownloadTokenCache, IRepository<Vehicle, Guid> vehicleRepository, IOwnerRepository ownerRepository, ICompanyRepository companyRepository, IVehicleRepository vehicleRepository2)
        {
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _transactionRepository = transactionRepository;
            _transactionManager = transactionManager;
            _vehicleRepository = vehicleRepository;
            _ownerRepository = ownerRepository;
            _companyRepository = companyRepository;
            _vehicleRepository2 = vehicleRepository2;
        }

        public virtual async Task<PagedResultDto<TransactionWithNavigationPropertiesDto>> GetListAsync(GetTransactionsInput input)
        {
            var totalCount = await _transactionRepository.GetCountAsync(input.FilterText, input.PriceMin, input.PriceMax, input.LitersMin, input.LitersMax, input.DateMin, input.DateMax, input.VehicleId, input.CompanyName);
            var items = await _transactionRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.PriceMin, input.PriceMax, input.LitersMin, input.LitersMax, input.DateMin, input.DateMax, input.VehicleId, input.CompanyName, input.Sorting, input.MaxResultCount, input.SkipCount);

            var newitems = ObjectMapper.Map<List<TransactionWithNavigationProperties>, List<TransactionWithNavigationPropertiesDto>>(items);

            foreach (var item in newitems)
            {
                item.Transaction.CompanyName = await GetCompanyNameByVehicleId(item.Vehicle.Id); //await this
            }

            return new PagedResultDto<TransactionWithNavigationPropertiesDto>
            {
                TotalCount = totalCount,
                Items = newitems
            };
        }

        public virtual async Task<PagedResultDto<ReportWithNavigationPropertiesDto>> GetReportListAsync(GetReportsInput input)
        {
            var totalCount = await _transactionRepository.GetReportCountAsync(input.FilterText, input.DateMin, input.DateMax, input.VehicleId, input.CompanyName);
            var items = await _transactionRepository.GetReportListWithNavigationPropertiesAsync(input.FilterText, input.DateMin, input.DateMax, input.VehicleId, input.CompanyName, input.Sorting, input.MaxResultCount, input.SkipCount);

            //var newitems = ObjectMapper.Map<List<ReportWithNavigationProperties>, List<ReportWithNavigationPropertiesDto>>(items); //bro cant map this

            var newitems = new List<ReportWithNavigationPropertiesDto>();
            foreach (var item in items)
            {
                var newitem = new ReportWithNavigationPropertiesDto
                {
                    // newitem.Report = ObjectMapper.Map<Report, ReportDto>(item.Report); //bro cant map this
                    Report = new ReportDto
                    {
                        Id = item.Report.Id,
                        TotalPrice = item.Report.TotalPrice,
                        TotalLiters = item.Report.TotalLiters,
                        VehicleId = item.Report.VehicleId,
                        CompanyName = item.Report.CompanyName,
                        TotalTransactions = item.Report.TotalTransactions,
                        AveragePrice = item.Report.AveragePrice,
                        AverageLiters = item.Report.AverageLiters,
                        AveragePricePerLiter = item.Report.AveragePricePerLiter,
                        AverageLitersPerTransaction = item.Report.AverageLitersPerTransaction,
                        AveragePricePerTransaction = item.Report.AveragePricePerTransaction,
                        AverageLitersPerPrice = item.Report.AverageLitersPerPrice,
                        ConcurrencyStamp = item.Report.ConcurrencyStamp
                    },


                    Vehicle = ObjectMapper.Map<Vehicle, VehicleDto>(item.Vehicle)
                };
                newitems.Add(newitem);
            }
            
            foreach (var item in newitems)
            {
                item.Report.CompanyName = await GetCompanyNameByVehicleId(item.Vehicle.Id); //await this
            }

            return new PagedResultDto<ReportWithNavigationPropertiesDto>
            {
                TotalCount = totalCount,
                Items = newitems
            };
        }

        public virtual async Task<TransactionWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return ObjectMapper.Map<TransactionWithNavigationProperties, TransactionWithNavigationPropertiesDto>
                (await _transactionRepository.GetWithNavigationPropertiesAsync(id));
        }

        public virtual async Task<TransactionDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<Transaction, TransactionDto>(await _transactionRepository.GetAsync(id));
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetVehicleLookupAsync(LookupRequestDto input)
        {
            var query = (await _vehicleRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.Plate != null &&
                         x.Plate.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<Vehicle>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Vehicle>, List<LookupDto<Guid>>>(lookupData)
            };
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetCompanyNameLookupAsync(LookupRequestDto input)
        {
            var query = (await _companyRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.Name != null &&
                         x.Name.Contains(input.Filter));
            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<Company>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Company>, List<LookupDto<Guid>>>(lookupData)
            };
        }

        [Authorize(Vehman2Permissions.Transactions.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _transactionRepository.DeleteAsync(id);
        }

        [Authorize(Vehman2Permissions.Transactions.Create)]
        public virtual async Task<TransactionDto> CreateAsync(TransactionCreateDto input)
        {
            if (input.VehicleId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["Vehicle"]]);
            }

            var transaction = await _transactionManager.CreateAsync(
            input.VehicleId, input.Price, input.Liters, input.Date
            );

            return ObjectMapper.Map<Transaction, TransactionDto>(transaction);
        }

        [Authorize(Vehman2Permissions.Transactions.Edit)]
        public virtual async Task<TransactionDto> UpdateAsync(Guid id, TransactionUpdateDto input)
        {
            if (input.VehicleId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["Vehicle"]]);
            }

            var transaction = await _transactionManager.UpdateAsync(
            id,
            input.VehicleId, input.Price, input.Liters, input.Date, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<Transaction, TransactionDto>(transaction);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(TransactionExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            
            var transactions = await _transactionRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.PriceMin, input.PriceMax, input.LitersMin, input.LitersMax, input.DateMin, input.DateMax);

            var companyNames = new Dictionary<Guid, string>();
            foreach (var item in transactions)
            {
                if (!companyNames.ContainsKey(item.Vehicle.Id))
                {
                    companyNames.Add(item.Vehicle.Id, await GetCompanyNameByVehicleId(item.Vehicle.Id));
                }
            }

            var items = transactions.Select( item => new
            {
                Price = item.Transaction.Price,
                Liters = item.Transaction.Liters,
                Date = item.Transaction.Date,

                Vehicle = item.Vehicle?.Plate,
                CompanyName = companyNames[item.Vehicle.Id],
            });



            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(items);
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "Transactions.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetReportListAsExcelFileAsync(ReportExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var reports = await _transactionRepository.GetReportListWithNavigationPropertiesAsync(input.FilterText, input.DateMin, input.DateMax, input.VehicleId, input.CompanyName);

            var companyNames = new Dictionary<Guid, string>();
            foreach (var item in reports)
            {
                if (!companyNames.ContainsKey(item.Vehicle.Id))
                {
                    companyNames.Add(item.Vehicle.Id, await GetCompanyNameByVehicleId(item.Vehicle.Id));
                }
            }

            var items = reports.Select( item => new
            {
                Araç = item.Vehicle?.Plate,
                Şirket = companyNames[item.Vehicle.Id],
                ToplamFiyat = item.Report.TotalPrice,
                ToplamLitre = item.Report.TotalLiters,
                Toplamİşlem = item.Report.TotalTransactions,
                OrtalamaFiyat = item.Report.AveragePrice,
                OrtalamaLitre = item.Report.AverageLiters,
                OrtalamaFiyatBölüLitre = item.Report.AveragePricePerLiter,
                OrtalamaLitreBölüİşlem = item.Report.AverageLitersPerTransaction,
                OrtalamaFiyatBölüİşlem = item.Report.AveragePricePerTransaction,
                OrtalamaLitreBölüFiyat = item.Report.AverageLitersPerPrice,
            });

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(items);
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "Reports.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new TransactionExcelDownloadTokenCacheItem { Token = token },
                new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30)
                });

            return new DownloadTokenResultDto
            {
                Token = token
            };
        }

        private async Task<string> GetCompanyNameByVehicleId(Guid vehicleId)
        {
            var vehicle = await _vehicleRepository2.GetAsync(vehicleId);
            var owner = await _ownerRepository.GetAsync(vehicle.OwnerId);
            var company = await _companyRepository.GetAsync(owner.CompanyId);
            if (company == null)
            {
                return "No Company";
            }
            return company.Name;
        }
    }
}