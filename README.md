# CreditCardValidation

The Project CreditCardValidator fulfil all the given scenarios.
A common check that is performed upfront is to validate the card type based on the starting digits and length of card number. The main patterns that we care about are as follows:
+============+=============+===============+
| Card Type  | Begins With | Number Length |
+============+=============+===============+
| AMEX       | 34 or 37    | 15            |
+------------+-------------+---------------+
| Discover   | 6011        | 16            |
+------------+-------------+---------------+
| MasterCard | 51-55       | 16            |
+------------+-------------+---------------+
| Visa       | 4           | 13 or 16      |
+------------+-------------+---------------+
All of these card types also generate numbers such that they can be validated by the Luhn algorithm, so that's the second check systems usually try. The steps are:
1.	Starting with the next to last digit and continuing with every other digit going back to the beginning of the card, double the digit
2.	Sum all doubled and untouched digits in the number. For digits greater than 9 you will need to split them and sum the independently (i.e. "10", 1 + 0).
3.	If that total is a multiple of 10, the number is valid.
Project had followed the Clean Architectural Pattern ,VS 2022 and .Net 6(.Net Core).

