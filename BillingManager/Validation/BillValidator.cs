using Api.Dto;
using FluentValidation;

namespace Api.Validation
{
    public class BillValidator : AbstractValidator<BillDto>
    {
        public BillValidator()
        {
            RuleFor(x => x.BillNumber)
                .NotEmpty().WithMessage("Bill number is required.")
                .Must(IsValidBillNumber).WithMessage("Invalid bill number.");
        }

        private bool IsValidBillNumber(string billNumber)
        {
            // Split the bill number into three parts
            var parts = billNumber.Split('-');

            if (parts.Length != 3 || parts[0].Length != 3 || parts[1].Length != 13 || parts[2].Length != 2)
            {
                return false; // Invalid format
            }

            // Extract the identification code (AAA), bill number (BBBBBBBBBBBBB), and control number (CC)
            string identificationCode = parts[0];
            string billDigits = parts[1];
            string controlNumber = parts[2];

            // Combine identification code and bill number
            long combinedNumber = long.Parse(identificationCode + billDigits);

            // Multiply by 100
            combinedNumber *= 100;

            // Calculate the remainder when divided by 97
            long remainder = combinedNumber % 97;

            // Calculate the expected control number
            int expectedControlNumber = 98 - (int)remainder;

            // Compare the calculated control number with the provided control number
            return expectedControlNumber == int.Parse(controlNumber);
        }
    }
}
