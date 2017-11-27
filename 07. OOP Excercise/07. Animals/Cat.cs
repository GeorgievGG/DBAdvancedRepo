using System;

public class Cat : Animal
{
    public Cat(string name, int age, string gender)
    {
        this.Name = name;
        this.Age = age;
        this.Gender = gender;
        this.AnimalType = "Cat";
        this.Sound = "Meow meow";
    }
    private string name;
    private int age;
    private string gender;
    private string animalType;
    private string sound;
    public override string Name
    {
        get
        {
            return this.name;
        }

        set
        {
            if (string.IsNullOrWhiteSpace(value) || value == string.Empty)
            {
                throw new ArgumentException("Invalid input!");
            }
            this.name = value;
        }
    }
    public override int Age
    {
        get
        {
            return this.age;
        }

        set
        {
            if (value < 1)
            {
                throw new ArgumentException("Invalid input!");
            }
            this.age = value;
        }
    }
    public override string Gender
    {
        get
        {
            return this.gender;
        }

        set
        {
            if (string.IsNullOrWhiteSpace(value) || value == string.Empty)
            {
                throw new ArgumentException("Invalid input!");
            }
            this.gender = value;
        }
    }
    public virtual string AnimalType
    {
        get
        {
            return this.animalType;
        }
        set
        {
            this.animalType = value;
        }
    }
    public virtual string Sound
    {
        get
        {
            return this.sound;
        }
        set
        {
            this.sound = value;
        }
    }

    public override string ProduceSound()
    {
        return $"{this.AnimalType}{Environment.NewLine}{base.ProduceSound()}{Environment.NewLine}{this.Sound}";
    }
}