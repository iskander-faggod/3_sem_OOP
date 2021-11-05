using System;
using Banks.Entities.AccountsModel.Builders.Interfaces;
using Banks.Entities.AccountsModel.Creator;
using Banks.Tools;

namespace Banks.Entities.AccountsModel.Builders
{
    public class DepositAccountBuilder : IAccountBuilder
    {
        private Guid? _accountId = null;
        private decimal? _lowPercent = null;
        private decimal? _middlePercent = null;
        private decimal? _highPercent = null;
        private DateTime? _depositUnlockDate = null;

        public IAccount Build()
        {
            if (!_accountId.HasValue) throw new BanksException($"Required field {nameof(_accountId)} is missing");
            if (!_lowPercent.HasValue) throw new BanksException($"Required field {nameof(_lowPercent)} is missing");
            if (!_middlePercent.HasValue)
                throw new BanksException($"Required field {nameof(_middlePercent)} is missing");
            if (!_highPercent.HasValue) throw new BanksException($"Required field {nameof(_highPercent)} is missing");
            if (!_depositUnlockDate.HasValue)
                throw new BanksException($"Required field {nameof(_depositUnlockDate)} is missing");

            return new DepositAccount(_lowPercent.Value, _middlePercent.Value, _highPercent.Value, _accountId.Value, _depositUnlockDate.Value);
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
            return ThrowInvalidOperation(nameof(SetPercent));
        }

        public IAccountBuilder SetCommission(decimal commission)
        {
            return ThrowInvalidOperation(nameof(SetCommission));
        }

        public IAccountBuilder SetUnlockDate(DateTime dateTime)
        {
            _depositUnlockDate = dateTime;
            return this;
        }

        public IAccountBuilder SetLowPercent(decimal lowPercent)
        {
            if (lowPercent > _middlePercent)
            {
                throw new BanksException(
                    $" LowPercent - {lowPercent} can not be more then MiddlePercent - {_middlePercent}");
            }

            if (lowPercent > _highPercent)
            {
                throw new BanksException(
                    $" LowPercent - {lowPercent} can not be more then HighPercent - {_highPercent}");
            }

            _lowPercent = lowPercent;
            return this;
        }

        public IAccountBuilder SetMiddlePercent(decimal middlePercent)
        {
            if (middlePercent < _lowPercent)
            {
                throw new BanksException(
                    $" LowPercent - {_lowPercent} can not be more then MiddlePercent - {_middlePercent}");
            }

            if (middlePercent > _highPercent)
            {
                throw new BanksException(
                    $" middlePercent - {middlePercent} can not be more then HighPercent - {_highPercent}");
            }

            _middlePercent = middlePercent;
            return this;
        }

        public IAccountBuilder SetHighPercent(decimal highPercent)
        {
            if (highPercent < _lowPercent)
            {
                throw new BanksException(
                    $" LowPercent - {_lowPercent} can not be more then highPercent - {highPercent}");
            }

            if (highPercent < _middlePercent)
            {
                throw new BanksException(
                    $" middlePercent - {_middlePercent} can not be more then HighPercent - {_highPercent}");
            }

            _highPercent = highPercent;
            return this;
        }

        private IAccountBuilder ThrowInvalidOperation(string operationName)
        {
            throw new BanksException($"{operationName} is invalid operation for a deposit account");
        }
    }
}