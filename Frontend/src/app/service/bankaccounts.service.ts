import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BankAccount } from '../models/bankaccount.model';

@Injectable({
  providedIn: 'root'
})
export class BankaccountsService {

baseUrl = 'https://localhost:7276/api/bankaccounts';

  constructor(private http: HttpClient) { }

  //get all accounts
  getAllAccounts(): Observable<BankAccount[]> {
    return this.http.get<BankAccount[]>(this.baseUrl);
  }

  addBankAccount(bankaccount: BankAccount): Observable<BankAccount> {
    bankaccount.id = '00000000-0000-0000-0000-000000000000';
    return this.http.post<BankAccount>(this.baseUrl, bankaccount)
  }

  deleteBankAccount(id: string): Observable<BankAccount>{
    return this.http.delete<BankAccount>(this.baseUrl + '/' + id);
  }

  updateBankAccount(bankAccount: BankAccount): Observable<BankAccount>{
    return this.http.put<BankAccount>(this.baseUrl + '/' + bankAccount.id, bankAccount)
  }
}
