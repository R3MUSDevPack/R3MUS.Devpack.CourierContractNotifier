using System;
using System.Collections.Generic;

namespace R3MUS.Devpack.CourierContractNotifier.Models
{
    public class NotificationRequest
    {
        public const string OutstandingStatus = "outstanding";
        public const string CompletedStatus = "finished";
        public const string AcceptedStatus = "in_progress";
        public const string DeletedStatus = "deleted";
        
        private const string _yellow = "#DBEF04";
        private const string _amber = "#EFAD04";
        private const string _green = "#00FF00";
        private const string _red = "#FF0000";

        public long ContractId { get; set; }
        public DateTime Issued { get; set; }
        public DateTime? Accepted { get; set; }
        public DateTime? Completed { get; set; }
        public Entity Issuer { get; set; }
        public string Status { get; set; }
        public Endpoint Origin { get; set; }
        public Endpoint Destination { get; set; }
        public decimal Volume { get; set; }
        public decimal Reward { get; set; }
        public decimal Collateral { get; set; }
        public decimal CorrectReward
        {
            get
            {
                return (Volume * Properties.Settings.Default.PaymentPerM3);
            }
        }
        public bool PaymentIncorrect
        {
            get
            {
                return Reward < CorrectReward;
            }
        }
        public bool SizeIncorrect
        {
            get
            {
                return Volume > Properties.Settings.Default.MaximumSize;
            }
        }
        public bool HasCollateral
        {
            get
            {
                return Collateral > 0.00M;
            }
        }

        public string GetTitle()
        {
            switch (Status)
            {
                case OutstandingStatus:
                    return string.Format(Properties.Resources.MessageFormatLine1, Status, Issued.ToString("dd/MM/yyyy HH:mm:ss"));
                    break;
                case AcceptedStatus:
                    return string.Format(Properties.Resources.MessageFormatLine1, Status, Accepted.Value.ToString("dd/MM/yyyy HH:mm:ss"));
                    break;
                case CompletedStatus:
                    return string.Format(Properties.Resources.MessageFormatLine1, Status, Completed.Value.ToString("dd/MM/yyyy HH:mm:ss"));
                    break;
                default:
                    return string.Format(Properties.Resources.MessageFormatLine1a, Status);
            };
        }

        public string GetColour()
        {
            if (Status == NotificationRequest.OutstandingStatus)
            {
                if (PaymentIncorrect || SizeIncorrect || HasCollateral)
                {
                    return _red;
                }
                else
                {
                    return _amber;
                }
            }
            else if (Status == NotificationRequest.AcceptedStatus)
            {
                return _yellow;
            }
            else if (Status == NotificationRequest.CompletedStatus)
            {
                return _green;
            }
            else
            {
                return _red;
            }
        }

        public string GetMessageText()
        {
            var result = new List<string>();

            result.Add(string.Format(Properties.Resources.MessageFormatLine2, Issuer.Name));
            result.Add(string.Format(Properties.Resources.MessageFormatLine3, Reward.ToString("N2")));
            result.Add(string.Format(Properties.Resources.MessageFormatLine7, CorrectReward.ToString("N2")));
            result.Add(string.Format(Properties.Resources.MessageFormatLine4, Volume.ToString("N2")));
            result.Add(string.Format(Properties.Resources.MessageFormatLine5, Origin.Name));
            result.Add(string.Format(Properties.Resources.MessageFormatLine6, Destination.Name));
            if (PaymentIncorrect || HasCollateral || SizeIncorrect)
            {
                result.Add(string.Empty);
            }
            if (PaymentIncorrect)
            {
                result.Add(Properties.Resources.IncorrectPayment);
            }
            if (HasCollateral)
            {
                result.Add(string.Format(Properties.Resources.CollateralSet, Collateral.ToString("N2")));
            }
            if (SizeIncorrect)
            {
                result.Add(string.Format(Properties.Resources.IncorrectSize, Properties.Settings.Default.MaximumSize.ToString("N2")));
            }

            return string.Join("\n", result);
        }
    }
}
