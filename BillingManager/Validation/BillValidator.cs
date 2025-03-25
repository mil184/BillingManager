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
            var parts = billNumber.Split('-');

            if (parts.Length != 3 || parts[0].Length != 3 || parts[1].Length != 13 || parts[2].Length != 2)
            {
                return false;
            }

            string identificationCode = parts[0];
            string billDigits = parts[1];
            string controlNumber = parts[2];

            long combinedNumber = long.Parse(identificationCode + billDigits);

            combinedNumber *= 100;

            long remainder = combinedNumber % 97;

            int expectedControlNumber = 98 - (int)remainder;

            return expectedControlNumber == int.Parse(controlNumber);
        }
    }
}
