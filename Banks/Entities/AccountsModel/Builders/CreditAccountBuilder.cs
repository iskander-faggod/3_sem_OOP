#nullable enable
using System;
using Banks.Entities.AccountsModel.Builders.Interfaces;
using Banks.Entities.AccountsModel.Creator;
using Banks.Tools;

namespace Banks.Entities.AccountsModel.Builders
{
    public class CreditAccountBuilder : IAccountBuilder
    {
        private decimal? _limit = null;
        private Guid? _accountId = null;
        private decimal? _commission = null;

        public IAccount Build()
        {
            if (!_accountId.HasValue) throw new BanksException($"Required field {nameof(_accountId)} is missing");
            if (!_limit.HasValue) throw new BanksException($"Required field {nameof(_limit)} is missing");
            if (!_commission.HasValue) throw new BanksException($"Required field {nameof(_commission)} is missing");
            return new CreditAccount(_limit.Value, _commission.Value, _accountId.Value);
        }

        public IAccountBuilder SetAccountId(Guid id)
        {
            if (id == default) throw new BanksException($"Field {nameof(id)} is invalid");
            _accountId = id;
            return this;
        }

        public IAccountBuilder SetLimit(decimal limit)
        {
            if (limit < 0) throw new BanksException($"Field {nameof(limit)} is invalid");
            _limit = limit;
            return this;
        }

        public IAccountBuilder SetUnlockDate(DateTime dateTime)
        {
            return ThrowInvalidOperation(nameof(SetUnlockDate));
        }

        public IAccountBuilder SetPercent(decimal percent)
        {
            return ThrowInvalidOperation(nameof(SetPercent));
        }

        public IAccountBuilder SetCommission(decimal commission)
        {
            if (commission < 0) throw new BanksException($"Field {nameof(commission)} is invalid");
            _commission = commission;
            return this;
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
            throw new BanksException($"{operationName} is invalid operation for a credit account");
        }
    }
}