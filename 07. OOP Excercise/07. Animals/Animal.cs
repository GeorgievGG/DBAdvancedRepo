using System;

public abstract class Animal : ISoundProducible<Animal>
{
    public abstract string Name { get; set; }
    public abstract int Age { get; set; }
    public abstract string Gender { get; set; }

    public virtual string ProduceSound()
    {
        return $"{this.Name} {this.Age} {this.Gender}";
    }
}