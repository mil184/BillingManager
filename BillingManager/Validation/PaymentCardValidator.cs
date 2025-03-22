using Api.Dto;
using FluentValidation;
using System.Text.RegularExpressions;

namespace Api.Validation
{
    public class PaymentCardValidator : AbstractValidator<PaymentDto>
    {
        public PaymentCardValidator()
        {
            RuleFor(x => x.Pan)
                .NotEmpty().WithMessage("PAN is required.")
                .Must(IsValidCreditCard).WithMessage("Invalid credit card number.")
                .Must(x => GetCardType(x) != "Unknown").WithMessage("Unsupported card type.");

            RuleFor(x => x.Cvv)
                .NotEmpty().WithMessage("CVV is required.")
                .Must((card, cvv) => IsValidCvv(card.Pan, cvv))
                .WithMessage("Invalid CVV for the given card type.");
        }

        public bool IsValidCreditCard(string pan)
        {
            var sum = 0;
            var shouldApplyDouble = true;
            for (var index = pan.Length - 2; index >= 0; index--)
            {
                var currentDigit = (Int32)Char.GetNumericValue(pan, index);
                if (shouldApplyDouble)
                {
                    if (currentDigit > 4)
                    {
                        sum += currentDigit * 2 - 9;
                    }
                    else
                    {
                        sum += currentDigit * 2;
                    }
                }
                else
                {
                    sum += currentDigit;
                }
                shouldApplyDouble = !shouldApplyDouble;
            }
            var checkDigit = 10 - (sum % 10);

            return Char.GetNumericValue(pan[^1]) == checkDigit;
        }

        private static bool IsValidCvv(string cardNumber, string cvv)
        {
            string cardType = GetCardType(cardNumber);

            return cardType switch
            {
                "Visa" or "MasterCard" => Regex.IsMatch(cvv, @"^\d{3}$"),
                "American Express" => Regex.IsMatch(cvv, @"^\d{4}$"),
                _ => false
            };
        }

        private static string GetCardType(string cardNumber)
        {
            if (Regex.IsMatch(cardNumber, @"^4(\d{12}|\d{15})$"))
                return "Visa";
            if (Regex.IsMatch(cardNumber, @"^5[1-5]\d{14}$"))
                return "MasterCard";
            if (Regex.IsMatch(cardNumber, @"^3[47]\d{13}$"))
                return "American Express";

            return "Unknown";
        }
    }
}
