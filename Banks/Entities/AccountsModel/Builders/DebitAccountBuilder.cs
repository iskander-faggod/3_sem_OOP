using System;
using Banks.Entities.AccountsModel.Builders.Interface;
using Banks.Entities.AccountsModel.Creator;
using Banks.Tools;

namespace Banks.Entities.AccountsModel.Builders
{
    public class DebitAccountBuilder : IAccountBuilder
    {
        private decimal? _deposit = null;
        private decimal? _percent = null;
        private Guid? _accountId = null;

        public IAccount Build()
        {
            if (!_accountId.HasValue) throw new BanksException($"Required field {nameof(_accountId)} is missing");
            if (!_percent.HasValue) throw new BanksException($"Required field {nameof(_percent)} is missing");
            if (!_deposit.HasValue)
                throw new BanksException($"Required field {nameof(_deposit)} is missing");

            return new DebitAccount(_deposit.Value, _percent.Value, _accountId.Value);
        }

        public IAccountBuilder SetAccountId(Guid id)
        {
            if (id == default) throw new BanksException($"Field {nameof(id)} is invalid");
            _accountId = id;
            return this;
        }

        public IAccountBuilder SetLimit(decimal limit)
        {
            return ThrowInvalidOperation(nameof(SetLimit));
        }

        public IAccountBuilder SetPercent(decimal percent)
        {
            if (percent < 0) throw new BanksException($"Field {nameof(percent)} is invalid");
            _percent = percent;
            return this;
        }

        public IAccountBuilder SetCommission(decimal commission)
        {
            return ThrowInvalidOperation(nameof(SetCommission));
        }

        public IAccountBuilder SetLowPercent(decimal lowPercent)
        {
            return ThrowInvalidOperation(nameof(SetLowPercent));
        }

        public IAccountBuilder SetMiddlePercent(decimal middlePercent)
        {
            return ThrowInvalidOperation(nameof(SetMiddlePercent));
        }

        public IAccountBuilder SetHighPercent(decimal highPercent)
        {
            return ThrowInvalidOperation(nameof(SetHighPercent));
        }

        private IAccountBuilder ThrowInvalidOperation(string operationName)
        {
            throw new BanksException($"{operationName} is invalid operation for a debit account");
        }
    }
}