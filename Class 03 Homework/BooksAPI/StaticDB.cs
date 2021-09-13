using BooksAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksAPI
{
    public class StaticDB
    {
        public static List<Book> Books = new List<Book>()
        {
            new Book()
            {
                Author = "Milan Kundera",
                Title = "The unbearable lightness of being"
            },
            new Book()
            {
                Author = "Gabriel Garcia Marquez",
                Title = "Of love and other demons"
            },
            new Book()
            {
                Author = "Herman Hesse",
                Title = "Siddhartha"
            },
            new Book()
            {
                Author = "Friedrich Nietzsche",
                Title = "Thus spoke Zarathustra"
            },
            new Book()
            {
                Author = "Dan Brown",
                Title = "Origin"
            },

        };
    }
}
