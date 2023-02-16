using System;
using System.Collections.Generic;
using System.Linq;

internal class Student : IComparable<Student>
{
    private readonly string name;
    private readonly int warnCount;
    private List<int> marks;

    public Student(string name, int warnCount, List<int> marks)
    {
        this.name = name;
        this.marks = marks;
        this.warnCount = warnCount;
    }

    public int CompareTo(Student other)
    {
        double thisAvg = marks.Count == 0 ? 0 : (double) marks.Sum() / marks.Count;
        double coeff = thisAvg - warnCount;

        double otherAvg = other.marks.Count == 0 ? 0 : (double) other.marks.Sum() / other.marks.Count;
        double otherCoeff = otherAvg - other.warnCount;
        
        if (coeff > otherCoeff)
        {
            return 1;
        }

        if (coeff == otherCoeff)
        {
            return 0;
        }

        return -1;
    }

    public override string ToString()
    {
        double avg = marks.Count == 0 ? 0 : (double) marks.Sum() / marks.Count;
        double coeff = avg - warnCount;
        return $"Student. Name = {name}. Coefficient = {coeff:F3}";
    }

    public static Student operator +(Student student, int mark)
    {
        if (mark < 0 || mark > 10)
        {
            throw new ArgumentException("Incorrect mark");
        }
        
        student.marks.Add(mark);
        return student;
        var marks = new List<int>(student.marks) {mark};
        var newStudent = new Student(student.name, student.warnCount, marks);
        return newStudent;
    }

    public static Student operator -(Student student, int mark)
    {
        if (!student.marks.Contains(mark))
        {
            throw new ArgumentException("Incorrect mark");
        }

        student.marks.Remove(mark);
        return student;
        var newStudent = new Student(student.name, student.warnCount, student.marks);
        return newStudent;
    }

    public static Student Parse(string line)
    {
        string[] split = line.Split(";");
        return new Student(split[0], int.Parse(split[1]), new List<int>());
    }
}