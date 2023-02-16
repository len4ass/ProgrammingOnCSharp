internal struct Student
{
    public static int Calls { get; set; }
    public static int Students { get; set; }
    
    public int Id { get; set; }
    public int Height { get; set; }
    public int Math { get; set; }
    public int English { get; set; }
    public int PE { get; set; }

    public Student(int id, int height, int math, int english, int PE)
    {
        Id = id;
        Height = height;
        Math = math;
        English = english;
        this.PE = PE;
    }

    internal static Student Parse(string v)
    {
        var i = v.Split();
        Students += 1;
        return new Student(int.Parse(i[0]), int.Parse(i[1]), int.Parse(i[2]), int.Parse(i[3]), int.Parse(i[4]));
    }

    public int CompareTo(Student other)
    {
        Calls += 1;
        if (Calls > Students)
        {
            if (PE > other.PE)
            {
                return 1;
            }

            if (PE == other.PE)
            {
                if (Height > other.Height)
                {
                    return 1;
                }
                
                return 0;
            }

            return -1;
        }

        if (Math > other.Math)
        {
            return 1;
        }

        if (Math == other.Math)
        {
            if (English > other.English)
            {
                return 1;
            }
            
            return 0;
        }

        return -1;
    }

    public override string ToString()
    {
        return Id.ToString();
    }
}