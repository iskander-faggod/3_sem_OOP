using System;
using Banks.Entities.AccountsModel.Creator;

namespace Banks.Entities.AccountsModel.Builders.Interfaces
{
    public interface IAccountBuilder
    {
         IAccount Build();

         IAccountBuilder SetAccountId(Guid id);
         IAccountBuilder SetLimit(decimal limit);
         IAccountBuilder SetUnlockDate(DateTime dateTime);
         IAccountBuilder SetPercent(decimal percent);
         IAccountBuilder SetCommission(decimal commission);
         IAccountBuilder SetLowPercent(decimal lowPercent);
         IAccountBuilder SetMiddlePercent(decimal middlePercent);
         IAccountBuilder SetHighPercent(decimal highPercent);
    }
}