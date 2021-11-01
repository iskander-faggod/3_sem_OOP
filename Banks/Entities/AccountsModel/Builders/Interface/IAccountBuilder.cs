using System;
using Banks.Entities.AccountsModel.Creator;

namespace Banks.Entities.AccountsModel.Builders.Interface
{
    public interface IAccountBuilder
    {
        public IAccount Build();

        public IAccountBuilder SetAccountId(Guid id);
        public IAccountBuilder SetLimit(decimal limit);
        public IAccountBuilder SetPercent(decimal percent);
        public IAccountBuilder SetCommission(decimal commission);
        public IAccountBuilder SetLowPercent(decimal lowPercent);
        public IAccountBuilder SetMiddlePercent(decimal middlePercent);
        public IAccountBuilder SetHighPercent(decimal highPercent);
    }
}