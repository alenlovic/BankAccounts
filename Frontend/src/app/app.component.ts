import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { BankAccount } from './models/bankaccount.model';
import { BankaccountsService } from './service/bankaccounts.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent implements OnInit {
  title = 'bankaccounts';
  bankAccounts: BankAccount[] = [];
  bankAccount: BankAccount = {
    id: '',
    cardNumber: '',
    cardholderName: '',
    expiryMonth: '',
    expiryYear: '',
    cvv: '',
  };

  constructor(
    private toastr: ToastrService,
    private bankAccountsService: BankaccountsService
  ) {}

  ngOnInit(): void {
    this.getAllAccounts();
  }

  getAllAccounts() {
    this.bankAccountsService.getAllAccounts().subscribe((response) => {
      this.bankAccounts = response;
    });
  }

  onSubmit() {
    if (this.bankAccount.id === '') {
      this.bankAccountsService
        .addBankAccount(this.bankAccount)
        .subscribe((response) => {
          this.getAllAccounts();
          this.bankAccount = {
            id: '',
            cardNumber: '',
            cardholderName: '',
            expiryMonth: '',
            expiryYear: '',
            cvv: '',
          };
          this.toastr.success('You have succesfully registered a bank account');
        });
    } else {
      this.updateBankAccount(this.bankAccount);
    }
  }

  deleteCard(id: string) {
    this.bankAccountsService.deleteBankAccount(id).subscribe((response) => {
      this.getAllAccounts();
    });
    this.toastr.success('You have succesfully deleted a bank account');
  }

  populateForm(bankAccount: BankAccount) {
    this.bankAccount = bankAccount;
  }

  updateBankAccount(bankAccount: BankAccount) {
    this.bankAccountsService
      .updateBankAccount(bankAccount)
      .subscribe((response) => {
        this.getAllAccounts();
        this.bankAccount = {
          id: '',
          cardNumber: '',
          cardholderName: '',
          expiryMonth: '',
          expiryYear: '',
          cvv: '',
        };
      });
    this.toastr.success('You have succesfully updated a bank account');
  }
}
