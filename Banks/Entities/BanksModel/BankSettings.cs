using System;
using Banks.Tools;

namespace Banks.Entities
{
    public class BankSettings
    {
        public BankSettings(
            string name,
            decimal yearPercent,
            decimal belowFiftyThousandPercent,
            decimal betweenFiftyAndHundredThousandPercent,
            decimal aboveHundredThousandPercent,
            TimeSpan depositTimeDuration,
            DateTime depositUnlockDate,
            decimal transferLimit,
            decimal commission)
        {
            if (yearPercent <= 0)
            {
                throw new BanksException(
                    "Percent should be more than 0");
            }

            if (belowFiftyThousandPercent > betweenFiftyAndHundredThousandPercent)
                throw new BanksException("Below 50000 percent should be less then 50000-100000 percent");
            if (betweenFiftyAndHundredThousandPercent > aboveHundredThousandPercent)
                throw new BanksException("Between 50000-100000 percent should be less then 100000+ percent");
            if (depositTimeDuration <= TimeSpan.Zero)
                throw new BanksException("Deposit duration time should be more then 0");
            if (transferLimit < 0) throw new BanksException("Transfer limit should be more then 0");
            if (commission < 0) throw new BanksException("Commission should not be less then 0");
            YearPercent = yearPercent;
            BelowFiftyThousandPercent = belowFiftyThousandPercent;
            BetweenFiftyAndHundredThousandPercent = betweenFiftyAndHundredThousandPercent;
            AboveHundredThousandPercent = aboveHundredThousandPercent;
            DepositTimeDuration = depositTimeDuration;
            DepositUnlockDate = depositUnlockDate;
            TransferLimit = transferLimit;
            Commission = commission;
            Name = name;
        }

        public string Name { get; }
        public decimal Commission { get; }
        public decimal YearPercent { get; }
        public decimal BelowFiftyThousandPercent { get; }
        public decimal BetweenFiftyAndHundredThousandPercent { get; }
        public decimal AboveHundredThousandPercent { get; }
        public TimeSpan DepositTimeDuration { get; }
        public decimal TransferLimit { get; }
        public DateTime DepositUnlockDate { get; }
    }
}