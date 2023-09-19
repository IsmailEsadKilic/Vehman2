using Vehman2.Shared;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using Vehman2.Transactions;
using Volo.Abp.Content;
using Vehman2.Shared;

namespace Vehman2.Controllers.Transactions
{
    [RemoteService]
    [Area("app")]
    [ControllerName("Transaction")]
    [Route("api/app/transactions")]

    public class TransactionController : AbpController, ITransactionsAppService
    {
        private readonly ITransactionsAppService _transactionsAppService;

        public TransactionController(ITransactionsAppService transactionsAppService)
        {
            _transactionsAppService = transactionsAppService;
        }

        [HttpGet]
        public Task<PagedResultDto<TransactionWithNavigationPropertiesDto>> GetListAsync(GetTransactionsInput input)
        {
            return _transactionsAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("with-navigation-properties/{id}")]
        public Task<TransactionWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return _transactionsAppService.GetWithNavigationPropertiesAsync(id);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<TransactionDto> GetAsync(Guid id)
        {
            return _transactionsAppService.GetAsync(id);
        }

        [HttpGet]
        [Route("vehicle-lookup")]
        public Task<PagedResultDto<LookupDto<Guid>>> GetVehicleLookupAsync(LookupRequestDto input)
        {
            return _transactionsAppService.GetVehicleLookupAsync(input);
        }

        [HttpPost]
        public virtual Task<TransactionDto> CreateAsync(TransactionCreateDto input)
        {
            return _transactionsAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<TransactionDto> UpdateAsync(Guid id, TransactionUpdateDto input)
        {
            return _transactionsAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _transactionsAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("as-excel-file")]
        public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(TransactionExcelDownloadDto input)
        {
            return _transactionsAppService.GetListAsExcelFileAsync(input);
        }

        [HttpGet]
        [Route("download-token")]
        public Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            return _transactionsAppService.GetDownloadTokenAsync();
        }
    }
}