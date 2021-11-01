using System;
using Banks.Entities.AccountsModel.Builders;
using Banks.Entities.AccountsModel.Builders.Interface;
using Banks.Entities.AccountsModel.Creator;
using Banks.Tools;

namespace Banks.Entities.AccountsModel
{
    internal static class AccountBuilderFactory
    {
        internal static IAccountBuilder Create(AccountType accountType)
        {
            return accountType switch
            {
                AccountType.Credit => new CreditAccountBuilder(),
                AccountType.Debit => new DebitAccountBuilder(),
                AccountType.Deposit => new DepositAccountBuilder(),
                _ => throw new BanksException(
                    "Unknown account type",
                    new ArgumentOutOfRangeException(nameof(accountType)))
            };
        }
    }
}