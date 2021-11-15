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
            if (commission < 0) throw new BanksException("Commission should not be less then 0");
            YearPercent = yearPercent;
            BelowFiftyThousandPercent = belowFiftyThousandPercent;
            BetweenFiftyAndHundredThousandPercent = betweenFiftyAndHundredThousandPercent;
            AboveHundredThousandPercent = aboveHundredThousandPercent;
            DepositUnlockDate = depositUnlockDate;
            TransferLimit = transferLimit;
            Commission = commission;
            Name = name;
        }

        public string Name { get; set; }
        public decimal Commission { get; set; }
        public decimal YearPercent { get; set; }
        public decimal BelowFiftyThousandPercent { get; set; }
        public decimal BetweenFiftyAndHundredThousandPercent { get; set; }
        public decimal AboveHundredThousandPercent { get; set; }
        public decimal TransferLimit { get; set; }
        public DateTime DepositUnlockDate { get; set; }
    }
}