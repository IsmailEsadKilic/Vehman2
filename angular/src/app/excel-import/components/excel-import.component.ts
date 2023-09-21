import { Component, ViewChild } from '@angular/core';
import { Observable, take } from 'rxjs';
import * as XLSX from 'xlsx';
import { TransactionService } from '../../proxy/transactions/transaction.service';
import { Transaction, TransactionCreateDto } from '@proxy/transactions';
import { ToastrService } from 'ngx-toastr';
import { VehicleService } from '@proxy/vehicles';

@Component({
  selector: 'app-excel-import',
  templateUrl: './excel-import.component.html',
  styleUrls: ['./excel-import.component.scss']
})
export class ExcelImportComponent {
  maxDate: Date = new Date();
  bsConfig = { isAnimated: true, dateInputFormat: 'YYY.MM.DD', containerClass: 'theme-blue' };
  time: Date = new Date();

  transactions: TransactionCreateDto[] = [];

  daterow: number = 0;
  platerow: number = 3;
  litersrow: number = 5;
  pricerow: number = 6;

  @ViewChild('staticModal') staticModal: any;
  @ViewChild('confirmModal') confirmModal: any;

  vehicleAdded: boolean = false;
  unknownVehPlate: string | undefined;
  vehAdd: boolean = false;

  skipped: boolean = false;
  skipT: Transaction = {} as Transaction;

  constructor(private vehicleService: VehicleService, private transactionService: TransactionService,
    private toastr: ToastrService) { }

  ngOnInit(): void {
    console.log("hi");
    this.vehicleService.vehicleAdded$.subscribe({
      next: () => {
        this.vehicleAdded = true;
        this.staticModal?.hide();
        this.vehAdd = false;
      }
    })
  }

  onFileChange(event: any) {

    const file = event.target.files[0];

    // Check if the file is an Excel file
    if (!file || !file.name.endsWith('.xlsx') && !file.name.endsWith('.xls')) {
      console.log("Excel dosyası seçilmedi.");
      this.toastr.error("Excel dosyası seçilmedi.");
      return;
    }
    const reader = new FileReader();

    reader.onload = (e: any) => {
      const data = new Uint8Array(e.target.result);
      const workbook = XLSX.read(data, { type: 'array' });
      const sheetName = workbook.SheetNames[0]; // Assuming the sheet you want to import is the first one
      const sheet = workbook.Sheets[sheetName];

      const transactions: TransactionCreateDto[] = [];
      let rowIndex = 0; // Initialize rowIndex to track the row index

      XLSX.utils.sheet_to_json(sheet, { header: 1 }).forEach((row: any) => {
        if (rowIndex === 0) {
          rowIndex++; 
          return;
        }
        // Map the Excel columns to your Transaction model properties

        try {
          const transactionCreateDto: TransactionCreateDto = {
            date: (this.daterow == 0 && (!row[this.daterow -1])) ? this.formatDate(this.time) : row[this.daterow -1],   // Tarih
            vehicleId: "0",  // Plaka No
            plate: row[this.platerow -1],   // Plaka No
            price: parseFloat(row[this.pricerow -1]),   // Tutar (TL)
            liters: (this.litersrow == 0) ? undefined : parseFloat(row[this.litersrow -1]),   // Miktar (Lt)
          };
  
          transactions.push(transactionCreateDto);
          rowIndex++;
        } catch (error) {
          console.log(error);
          this.toastr.error("Excel dosyası okunamadı.");
          return;
        }

      });

      // Now you have an array of Transaction objects in the 'transactions' variable
      this.transactions = transactions;
    };

    reader.readAsArrayBuffer(file);
  }

  formatDate(date: Date): string {
      // 2023-09-06T00:00:00
    try {
      const day = String(date.getDate()).padStart(2, '0');
      const month = String(date.getMonth() + 1).padStart(2, '0'); // Months are 0-indexed
      const year = String(date.getFullYear());
      const hour = "00:00:00";
      return `${year}-${month}-${day}T${hour}`;
    } catch (error) {
      console.log(error);
      this.toastr.error("Tarih formatı hatalı.");
      return "";
    }
  }

  excelImport() {
    var fail = false;
    var added: string[] = [];
    var skip: number[] = [];
    //dev
    // console.log(this.transactions);
    // this.addEveryVehicleDEV();
    // return;
    //dev

    //if no transactions, stop the process.
    if (this.transactions.length == 0) {
      console.log("Alım bulunamadı.");
      this.toastr.error("Alım bulunamadı.");
      return;
    }
    this.vehicleService.getListAll().pipe(take(1)).subscribe({
      next: vehiclesresult => {
        var vehicles = vehiclesresult.items.map(vehicle => {
          return {
            id: vehicle.vehicle.id,
            plate: vehicle.vehicle.plate,
            model: vehicle.carModel,
            owner: vehicle.owner,
            fuel: vehicle.fuel
          }
        })
        this.transactionService.getListAll().pipe(take(1)).subscribe({
          next: async transactionsDBresult => {
            var transactionsDB = transactionsDBresult.items.map(tr => {
              return {
                id: tr.transaction.id,
                vehicleId: tr.vehicle.id,
                plate: tr.vehicle.plate,
                fuel: tr.vehicle.fuelId,
                date: tr.transaction.date,
                price: tr.transaction.price,
                liters: tr.transaction.liters
              }
            })
            //add vehicleId
            this.transactions.forEach(transaction => {
              vehicles.forEach(vehicle => {
                if (transaction.plate == vehicle.plate) {
                  transaction.vehicleId = vehicle.id;
                }
              })
            })
            // check if any transaction has vehicleId = 0. if so stop the process.  
            for (let i = 0; i < this.transactions.length; i++) {
              if (this.transactions[i].vehicleId == "0" || this.transactions[i].vehicleId == undefined || this.transactions[i].vehicleId == null) {
                // if in added array, continue
                if (added.includes((this.transactions[i].plate == undefined) ? "" : this.transactions[i].plate!)) {
                  continue;
                }
                console.log("Plaka bulunamadı.");
                console.log(this.transactions[i]);
                this.toastr.error("Plaka bulunamadı.");
                this.toastr.error(this.transactions[i].plate);
                
                try {
                  const userAdded = await new Promise<boolean>((resolve) => {
                    this.addUnknownVehicle(this.transactions[i].plate).subscribe((result) => {
                      resolve(result);
                    });
                  });
          
                  if (!userAdded) {
                    this.toastr.error("Araç ekleme işlemi iptal edildi.");
                    fail = true; // User canceled, stop the process
                    return; // Exit the loop
                  }
          
                  added.push(this.unknownVehPlate!);
                  this.toastr.success("Bilinmeyen araç eklendi.");
                } catch (error) {
                  console.log(error);
                  this.toastr.error("Plaka bulunamadı.");
                  fail = true;
                  return; // Exit the loop
                }
              }
            }
        
            //check if the same transaction exists in database. if so stop the process.
        
            for (let i = 0; i < this.transactions.length; i++) {
              for (let x = 0; x < transactionsDB.length; x++) {
                if (transactionsDB[x].vehicleId == this.transactions[i].vehicleId && transactionsDB[x].date == this.transactions[i].date ||
                  transactionsDB[x].vehicleId == this.transactions[i].vehicleId && transactionsDB[x].price == this.transactions[i].price) {
                  console.log("Bu alım zaten var.");
                  console.log(this.transactions[i]);
                  this.toastr.error("Bu alım zaten var.");
                  this.toastr.error(this.transactions[i].plate, this.transactions[i].date);

                  const skipped = await new Promise<boolean>((resolve) => {
                    this.confirmModalF(transactionsDB[x]).subscribe((result) => {
                      resolve(result);
                    });
                  });
      
                  if(skipped) {
                    skip.push(i)
                    console.log("Alım atlandı.");
                    this.toastr.info("Alım atlandı.");
                    continue;
                  }
                  this.toastr.error("Alım ekleme işlemi iptal edildi.");
                  fail = true;
                  console.log("Alım ekleme işlemi iptal edildi.");
                  return;
                }
              }
            }
        
            //add every transaction to database.
            if (!fail) {
              for (let i = 0; i < this.transactions.length; i++) {
                if (skip.includes(i)) {
                  continue;
                }
                console.log(this.transactions[i]);
                this.transactionService.create(this.transactions[i]).subscribe({
                  next: () => {
                    console.log("Transaction added.");
                  },
                  error: err => {
                    console.log(err);
                    this.toastr.error(err);
                  }
                })
              }
              this.toastr.success("Alımlar eklendi.");
            }
          }
        })
      }
    })
  }

  addUnknownVehicle(plate: string): Observable<boolean> {
    // Show the modal dialog
    this.vehAdd = true;
    this.unknownVehPlate = plate;
    this.vehicleAdded = false;
    this.staticModal.show();

    return new Observable<boolean>(observer => {
      // Listen for user's decision
      this.staticModal.onHidden.subscribe(() => {
        if (this.vehicleAdded) {
          // User added the vehicle
          observer.next(true);
          observer.complete();
          this.vehAdd = false;

        } else {
          // User canceled
          observer.next(false);
          observer.complete();
          this.vehAdd = false;
        }
      });
    });
  }

  skipF() {
    this.skipped = true;
    this.confirmModal.hide();
  }

  confirmModalF(transaction: Transaction): Observable<boolean> {
    this.skipped = false;
    this.skipT = transaction;
    console.log("skipt: " + this.skipT);
    this.confirmModal.show();
    return new Observable<boolean>(observer => {
      this.confirmModal.onHidden.subscribe(() => {
        if (this.skipped) {
          observer.next(true);
          observer.complete();
        } else {
          observer.next(false);
          observer.complete();
        }
      });
    });
  }

  // addEveryVehicleDEV() {
  //   this.transactions.forEach(transaction => {
  //     console.log(transaction.plate);
  //     var vehicleAdd: VehicleAdd = {
  //       plate: (transaction.plate) ? transaction.plate : "111111",
  //       ownerId: 1,
  //       companyId: 1,
  //       brandId: 1,
  //       modelId: 1,
  //       fuelTypeId: 1
  //     }
  //     this.vehicleService.addVehicle(vehicleAdd).subscribe({
  //       next: () => {
  //         console.log("Vehicle added.");
  //       }
  //     })
  //   })
  // }

  getExample() {
    var data  = [{
      "Teslimat Adresi": "ABC bilişim tek tic ve sa",
      "Maliyet Merkezi": "",
      "Plaka No": "34ABC123",
      "YakıtTipi": "KURŞUNSUZ",
      "Litre (LT)": 50,
      "Tutar (TL)": 300,
      "Tarih": "30.12.2021"
    }]
    var filename = "MeritAlımEklemeÖrnek.xlsx";
    var sheetName = "Alımlar";
    const ws: XLSX.WorkSheet = XLSX.utils.json_to_sheet(data);
    const wb: XLSX.WorkBook = XLSX.utils.book_new();
    XLSX.utils.book_append_sheet(wb, ws, sheetName);
    XLSX.writeFile(wb, filename);
  }
}
