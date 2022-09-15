var input = Console.ReadLine();

while (string.IsNullOrEmpty(input))
{
    //+ posiible verification of the string format

    Console.WriteLine("Please enter the numbers");
    input = Console.ReadLine();
};

var numberArray = input.Split(',', System.StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToArray();

var result = 0;

for (int i = 0; i < numberArray.Length; i++)
{
    var number = 0;
    if (i % 2 != 0)
    {
        number = numberArray[i] * 2;
    }
    else
    {
        number = numberArray[i];
    }
    while (number != 0)
    {
        result += number % 10;
        number /= 10;
    }

}


if (result  % 10 == 0)
{
    Console.WriteLine("Validation is passed.");
}
else
{
    Console.WriteLine("Validation is not passed.");
}













