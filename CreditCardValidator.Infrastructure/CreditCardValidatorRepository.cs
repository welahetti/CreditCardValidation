using CreditCardValidator.Application;
using CreditCardValidator.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditCardValidator.Infrastructure
{
    public class CreditCardValidatorRepository : ICreditCardValidatorRepository
    {
      static  List<CreditCardType> creditCardTypes = new List<CreditCardType>()
            { CreditCardType.VISA,
            CreditCardType.AMEX,
            CreditCardType.Discover ,
            CreditCardType.MasterCard};
        /// <summary>
        /// The ranges.
        /// </summary>
        private static List<CreditCardRange> ranges = CreateDefaults(creditCardTypes);

        

        /// <summary>
        /// Initializes a new instance of the <see cref="CreditCardProcessing.CreditCardRange"/> class.
        /// </summary>
        public CreditCardValidatorRepository()
        {
            //if (ranges.Count == 0)
            //{
            //    this.Issuer = CreditCardType.Unknown;
            //    this.RangeActive = true;
            //    this.UsesLuhn = true;
            //    this.Lengths = new List<int>();
            //    this.IssuerAccepted = true;
            //    ranges.Add(this);
            //}

            //if (!_flag)
            //{
            //    List<CreditCardType> creditCardTypes = new List<CreditCardType>()
            //{ CreditCardType.Visa,
            //CreditCardType.AmEx,
            //CreditCardType.Discover ,
            //CreditCardType.MasterCard};


            //    CreateDefaults(creditCardTypes);
            //}

        }


       



        /// <summary>
        /// Clear this instance's number ranges.
        /// </summary>
        public static void Clear()
        {
            ranges.Clear();
        }

        /// <summary>
        /// Creates all credit card types known as of 2016 March 06 as defaults.
        /// The included number ranges have been adapted from the list at:
        ///     https://en.wikipedia.org/wiki/Bank_card_number
        /// </summary>
        /// <param name="acceptedTypes">Accepted types.</param>
        public static List<CreditCardRange> CreateDefaults(List<CreditCardType> acceptedTypes = null)
        {
           
            List<CreditCardRange> acceptedranges = new List<CreditCardRange>();
          
            CreditCardRange AmEx = new CreditCardRange
            {
                Issuer = CreditCardType.AMEX,
                Numbers = "34,37",
                Lengths = { 15 },
                IssuerAccepted = acceptedTypes != null || acceptedTypes.Contains(CreditCardType.AMEX),
            };
            acceptedranges.Add (AmEx);

            CreditCardRange Discover = new CreditCardRange
            {
                Issuer = CreditCardType.Discover,
                Numbers = "6011",
                Lengths = { 16 },
                IssuerAccepted = acceptedTypes != null || acceptedTypes.Contains(CreditCardType.Discover),
            };
            acceptedranges.Add(Discover);


            CreditCardRange MasterCard = new CreditCardRange
            {
                Issuer = CreditCardType.MasterCard,
                Numbers = "51-55",
                Lengths = { 16 },
                IssuerAccepted = acceptedTypes != null || acceptedTypes.Contains(CreditCardType.MasterCard),
            };
            acceptedranges.Add (MasterCard);

            CreditCardRange Visa = new CreditCardRange
            {
                Issuer = CreditCardType.VISA,
                Numbers = "4",
                Lengths = { 13, 16 },
                IssuerAccepted = acceptedTypes != null || acceptedTypes.Contains(CreditCardType.VISA),
            };
            acceptedranges.Add (Visa);
            return acceptedranges;

        }

        /// <summary>
        /// Validates the card number.
        /// </summary>
        /// <returns>The card issuer.</returns>
        /// <param name="creditCardNumber">Credit card number.</param>
        public CreditCardRange ValidateCardNumber(string creditCardNumber)
        {
            CreditCardRange card = new CreditCardRange();
            int maxLength = 0;

            foreach (CreditCardRange  range in ranges)
            {
                int length;
                bool accepted = LengthIdentify(creditCardNumber, out length,range);

                if(accepted && length > maxLength)
                {
                    card = range;
                }
            }

            return card;
        }

        /// <summary>
        /// Validate the card number's structure, with information on the best match.
        /// </summary>
        /// <returns><c>true</c>, if the number is valid, <c>false</c> otherwise.</returns>
        /// <param name="creditCardNumber">Credit card number.</param>
        /// <param name="length">The length of the longest prefix matched. (Output)</param>
        public bool LengthIdentify(string creditCardNumber, out int length, CreditCardRange range)
        {
            int maxLength = 0;
            range.IssuerAccepted = true;

            // Skip if nobody cares
            if (!range.RangeActive)
            {
                length = 0;
                return false;
            }

            // Check the possibilities
            foreach (string num in range.rangeNumbers)
            {
                if (creditCardNumber.StartsWith(num) && num.Length > maxLength)
                {
                    maxLength = num.Length;
                }
            }

            // Validate number structure
            if (!range.Lengths.Contains(creditCardNumber.Length) && maxLength==0)
            {
                maxLength = 0;


            }
                  

            if (range.UseLuhn && !VerifyCreditCardNumberByLuhn(creditCardNumber))
            {
                
                range.IssuerAccepted = false;
            }

            if (range.Lengths.Contains(creditCardNumber.Length) && maxLength == 1)
            {
                range.IssuerAccepted = true;
            }

            length = maxLength;


            return  range.Issuer != CreditCardType.Unknown;
        }

        /// <summary>
        /// Verifies the credit card number by Luhn.
        /// </summary>
        /// <returns><c>true</c>, if credit card number by the Luhn checksum was verified, <c>false</c> otherwise.</returns>
        /// <param name="creditCardNumber">Credit card number.</param>
        private static bool VerifyCreditCardNumberByLuhn(string creditCardNumber)
        {
            int total = 0;

            for (int i = creditCardNumber.Length - 2; i > -1; i--)
            {
                char c = creditCardNumber[i];
                int val = (int)char.GetNumericValue(c);
                if ((creditCardNumber.Length - i) % 2 == 0)
                {
                    // Double every other digit
                    val *= 2;
                    if (val > 9)
                    {
                        // Pretend to add the digits
                        val -= 9;
                    }
                }

                total += val;
            }

            return total % 10 == (10 - char.GetNumericValue(creditCardNumber[creditCardNumber.Length - 1])) % 10;
        }
    }
}