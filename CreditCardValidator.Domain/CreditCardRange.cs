using CreditCardValidator.Domain;
using System;
using System.Collections.Generic;

namespace CreditCardValidator.Domain
{
   

    /// <summary>
    /// Credit card range.
    /// </summary>
    public class CreditCardRange
    {
      
        /// <summary>
        /// Flag on whether to use Luhn checksums.
        /// </summary>
        private static bool useLuhn = false;

        /// <summary>
        /// The issuer.
        /// </summary>
        private CreditCardType issuer;

        /// <summary>
        /// The range numbers.
        /// </summary>
        public  List<string> rangeNumbers;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreditCardProcessing.CreditCardRange"/> class.
        /// </summary>
        public CreditCardRange()
        {
            rangeNumbers = new List<string>();  
            issuer = new CreditCardType();
            this.Lengths = new List<int>();
            this.RangeActive = true;
            this.UseLuhn = true;
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="CreditCardProcessing.CreditCardRange"/> uses Luhn checksums.
        /// </summary>
        /// <value><c>true</c> if it uses Luhn; otherwise, <c>false</c>.</value>
        public  bool UseLuhn
        {
            get
            {
                return useLuhn;
            }

            set
            {
                useLuhn = value;
            }
        }

        /// <summary>
        /// Gets or sets this instance's issuer name.
        /// </summary>
        /// <value>The name of the card's issuer.</value>
        public string IssuerName { get; set; }

        /// <summary>
        /// Gets or sets this instance's issuer.
        /// </summary>
        /// <value>One of an enumeration of issuers found in the <see cref="CreditCardProcessing.CreditCardType"/> class.</value>
        public CreditCardType Issuer
        {
            get
            {
                return this.issuer;
            }

            set
            {
                this.issuer = value;
                if (value != CreditCardType.Unknown && string.IsNullOrWhiteSpace(this.IssuerName))
                {
                    this.IssuerName = this.issuer.ToString();
                }
            }
        }

        /// <summary>
        /// Sets the number prefix ranges.
        /// </summary>
        /// <value>The number prefix ranges, as a comma-delimited list.</value>
        public string Numbers
        {
            set
            {
                if (this.rangeNumbers == null)
                {
                    this.rangeNumbers = new List<string>();
                }

                this.rangeNumbers.Clear();
                foreach (var range in value.Split(','))
                {
                    var extremes = range.Trim().Split('-');
                    if (extremes.Length == 1)
                    {
                        // Not a range
                        this.rangeNumbers.Add(extremes[0]);
                        continue;
                    }

                    int low = -1, high = -1;
                    int.TryParse(extremes[0], out low);
                    int.TryParse(extremes[1], out high);
                    if (low == -1 || high == -1 || low > high || low.ToString().Length != low.ToString().Length)
                    {
                        // Malformed range
                        continue;
                    }

                    for (int i = low; i <= high; i++)
                    {
                        this.rangeNumbers.Add(i.ToString());
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the card number lengths.
        /// </summary>
        /// <value>The lengths.</value>
        public List<int> Lengths { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="CreditCardProcessing.CreditCardRange"/> range is active.
        /// </summary>
        /// <value><c>true</c> if range is in use; otherwise, <c>false</c>.</value>
        public bool RangeActive { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance issuer is accepted.
        /// </summary>
        /// <value><c>true</c> if this instance issuer is accepted by the vendor; otherwise, <c>false</c>.</value>
        public bool IssuerAccepted { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="CreditCardProcessing.CreditCardRange"/> uses Luhn.
        /// </summary>
        /// <value><c>true</c> if the issuer uses the Luhn checksum; otherwise, <c>false</c>.</value>
        public bool UsesLuhn { get; set; }

       
    }
}
