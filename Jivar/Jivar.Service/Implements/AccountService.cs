﻿using Jivar.BO.Models;
using Jivar.Repository.Interface;
using Jivar.Service.Interfaces;
using Task = System.Threading.Tasks.Task;

namespace Jivar.Service.Implements
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<bool> IsEmailExist(string email)
        {
            var account = await _accountRepository.GetAsync(ft => ft.Email == email);
            return account != null;
        }


        public async Task<bool> AddAccount(Account account)
        {

            return await _accountRepository.AddAsync(account);
        }

        public async Task<List<Account>> GetAccountsByIds(List<int> ids)
        {
            if (ids == null || !ids.Any())
                throw new ArgumentException("The list of IDs cannot be null or empty.");

            // Fetch accounts by IDs
            var accounts = await _accountRepository.GetAllAsync(account => ids.Contains(account.Id));

            // Check if all IDs are present
            var missingIds = ids.Except(accounts.Select(a => a.Id)).ToList();
            if (missingIds.Any())
                throw new Exception($"The following account with ids were not found: {string.Join(", ", missingIds)}");

            return accounts.ToList();
        }

        public async Task<Account> GetAccountById(int id)
        {
            var account = await _accountRepository.GetAsync(account => account.Id == id);

            if (account == null)
                throw new Exception($"Account with id: {id} not found");

            return account;
        }

    }
}
