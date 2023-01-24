import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { BankAccount } from 'src/app/models/bankaccount.model';
import { BankaccountsService } from 'src/app/service/bankaccounts.service';

@Component({
  selector: 'app-form',
  templateUrl: './form.component.html',
  styleUrls: ['./form.component.css'],
})
export class FormComponent implements OnInit {
  bankAccount: BankAccount = {
    id: '',
    cardNumber: '',
    cardholderName: '',
    expiryMonth: '',
    expiryYear: '',
    cvv: '',
  };
  bankAccounts: BankAccount[] = [];

  constructor(
    private bankAccountsService: BankaccountsService,
    private toastr: ToastrService
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

  updateBankAccount(bankAccount: BankAccount) {
    this.bankAccountsService
      .updateBankAccount(bankAccount)
      .subscribe((response) => {
        this.getAllAccounts();
      });
    this.toastr.success('You have succesfully updated a bank account');
  }
}
