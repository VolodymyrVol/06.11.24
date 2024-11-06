// Интерфейсы IEnumerable, IEnumerator
// Эти два интерфейса используются  для того, чтобы можно было применять цикл foreach по отношению к объекту класса

using System;
using System.Collections;
using System.Xml.Linq;
class Book : IComparable, ICloneable
{
    public string Name { get; set; }

    public string Author { get; set; }

    public Book(string name, string a)
    {
        this.Name = name;
        this.Author = a;
    }

    public Book() : this("", "") { }

    public void Show()
    {
        Console.WriteLine("\n{0}   {1}", Name, Author);
    }

    public void Input()
    {
        Console.WriteLine("\nВведите название книги: ");
        this.Name = Console.ReadLine();
        Console.WriteLine("\nВведите имя автора: ");
        this.Author = Console.ReadLine();
    }

    public int CompareTo(object obj)
    {
        if (obj is Book)
            return Name.CompareTo((obj as Book).Name);

        throw new NotImplementedException();
    }
    public object Clone()
    {
        return new Book(Name, Author);
    }
}

// IEnumerable предоставляет перечислитель, который поддерживает простой перебор элементов необобщенной коллекции
// IEnumerator поддерживает простой перебор по необобщенной коллекции
class Library //: IEnumerable , IEnumerator
{
    Book[] ar;
    int curpos = -1;
    public Library(int len)
    {
        ar = new Book[len];
        for (int i = 0; i < len; i++)
        {
            ar[i] = new Book();
        }
    }

    public Library() : this(1) { }

    public Library(Book[] books)
    {
        ar = new Book[books.Length];
        for (int i = 0; i < books.Length; i++)
        {
            ar[i] = new Book(books[i].Name, books[i].Author);
        }
    }

    public void InputBook()
    {
        for (int i = 0; i < ar.Length; i++)
            ar[i].Input();
    }

    public void ShowBooks()
    {
        for (int i = 0; i < ar.Length; i++)
            ar[i].Show();
    }


    public Library GetEnumerator()
    {
        // возвращается ссылка на объект класса, реализующего перечислитель
        return this;
    }

    // реализация перечислителя
    #region enumerator

    //Устанавливает перечислитель в его начальное положение, т. е. перед первым элементом коллекции
    public void Reset()
    {
        curpos = -1;
    }
    public object Current // Получает текущий элемент в коллекции
    {
        get
        {
            return ar[curpos];
        }
    }

    // Перемещает перечислитель к следующему элементу коллекции
    public bool MoveNext()
    {
        if (curpos < ar.Length - 1)
        {
            curpos++;
            return true;
        }
        else
        {
            this.Reset();
            return false;
        }

    }
    #endregion enumerator
}

class MainClass
{
    public static void Main()
    {
        Book[] c = new Book[6];
        c[0] = new Book("Аутсайдер", "Стивен Кинг");
        c[1] = new Book("Исскуство войны", "Сунь-Цзы");
        c[2] = new Book("Гарри Поттер", "Джоан Роулинг");
        c[3] = new Book("Властелин колец", "Дж.Р.Р. Толкин");
        c[4] = new Book("Унесенные ветром", "Маргарет Митчелл");
        c[5] = new Book("1984", "Джордж Оруэлл");
        foreach (Book temp in c)
            temp.Show();
        Library lg = new Library(c);
        foreach (Book temp in lg)
            temp.Show();
        foreach (Book temp in lg)
            temp.Show();
    }
}

