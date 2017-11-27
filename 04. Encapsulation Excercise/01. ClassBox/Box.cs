using System;

public class Box
{
    private decimal length;
    private decimal width;
    private decimal height;

    public Box(decimal length, decimal width, decimal height)
    {
        this.Length = length;
        this.Width = width;
        this.Height = height;
    }

    public decimal Surface => 2 * (Length * Width + Length * Height + Width * Height);
    public decimal LSurface => 2 * (Length * Height + Width * Height);
    public decimal Volume => Length * Width * Height;

    public decimal Length
    {
        get
        {
            return length;
        }

        private set
        {
            if (value <= 0)
            {
                throw new ArgumentException("Length cannot be zero or negative. ");
            }
            length = value;
        }
    }

    public decimal Width
    {
        get
        {
            return width;
        }

        private set
        {
            if (value <= 0)
            {
                throw new ArgumentException("Width cannot be zero or negative. ");
            }
            width = value;
        }
    }

    public decimal Height
    {
        get
        {
            return height;
        }

        private set
        {
            if (value <= 0)
            {
                throw new ArgumentException("Height cannot be zero or negative. ");
            }
            height = value;
        }
    }

    public override string ToString()
    {
        return $"Surface Area - {this.Surface:f2}{Environment.NewLine}Lateral Surface Area - {this.LSurface:f2}{Environment.NewLine}Volume - {this.Volume:f2}";
    }
}