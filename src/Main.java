import static java.lang.IO.print;
import static java.lang.IO.println;

void main() {
    Scanner sc = new Scanner(System.in);

    int grade = sc.nextInt();
    if (grade >= 0 && grade <= 59)
    {
        println("Незадовільно");
    }
    else if (grade >= 60 && grade <= 74)
    {
        println("Задовільно");
    }
    else if (grade >= 75 && grade <= 89)
    {
        println("Добре");
    }
    else if (grade >= 90 && grade <= 100)
    {
        println("Відмінно");
    }
    else
    {
        println("Grade must be between 0 and 100");
    }

    int number = sc.nextInt();
    if (number % 2 == 0)
    {
        println(number + " is even");
    }
    else
    {
        println(number + " is odd");
    }

    int sum = 0;
    double avg = 0;
    String status = "";
    boolean has_scholarship = false;
    for(int i = 0; i < 5; i++)
    {
        int grade1 = sc.nextInt();
        sum += grade1;
    }
    avg = (double)sum / 5;
    if (avg >= 90)
    {
        status = "Відмінник";
        has_scholarship = true;
    }
    else if (avg >= 75 && avg <= 89)
    {
        status = "Хорошист";
    }
    else if (avg < 75)
    {
        status = "Двієчник";
    }

    println("Student info: average grade: " + avg + " status: " + status + " Scholarship: " + has_scholarship);

    int num1 = 0;
    int num2 = 1;
    for (int i = 0; i < 15; i++)
    {
        println(num1 + " ");
        int num3 = num1 + num2;
        num1 = num2;
        num2 = num3;
    }

    int rows = sc.nextInt();

    for (int i = 0; i < rows; i++)
    {
        for (int k = 0; k < rows - i; k++)
        {
            print(" ");
        }

        for (int j = 0; j <= i * 2; j++)
        {
            print("*");
        }
        println();
    }

    for (int p = 2; p <= 5; p++)
    {
        boolean pIsPrime = true;

        for (int i = 2; i * i <= p; i++)
        {
            if (p % i == 0)
            {
                pIsPrime = false;
                break;
            }
        }

        if (pIsPrime)
        {
            int mersenne = (int)Math.pow(2, p) - 1;

            boolean mIsPrime = true;

            for (int i = 2; i * i <= mersenne; i++)
            {
                if (mersenne % i == 0)
                {
                    mIsPrime = false;
                    break;
                }
            }

            if (mIsPrime)
            {
                int perfectNumber = (int)Math.pow(2, p - 1) * mersenne;

                println(perfectNumber + " is a perfect number");
            }
        }
    }
}