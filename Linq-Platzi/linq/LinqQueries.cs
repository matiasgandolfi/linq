using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace linq
{
    public class LinqQueries
    {
        private List<Book> librosCollection = new List<Book>();
        public LinqQueries() 
        {
            using (StreamReader reader = new StreamReader("books.json"))
            {
                string json = reader.ReadToEnd();
                this.librosCollection = System.Text.Json.JsonSerializer.Deserialize<List<Book>>(json, new System.Text.Json.JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            }
        }


        public IEnumerable<Book> TodaLaColeccion()
        {
            return librosCollection;
        }



        /*---------------------------Where-------------------------------------------*/


        public IEnumerable<Book> LibrosDespesdel2000()
        {
            //Extenison method
            //return librosCollection.Where(p => p.PublishedDate.Year > 2000);

            //query extension

            return from l in librosCollection where l.PublishedDate.Year > 2000 select l;
        }



        public IEnumerable<Book> LibrosMas250Paginas()
        {
            //Expresion method
            //return librosCollection.Where(p => p.PageCount > 250 && p.Title.Contains("Action"));



            //query extension
            return from l in librosCollection where l.PageCount > 250 && l.Title.Contains("Action") select l;
        }




        /*-------------------------All & Any---------------------------------------------*/
        public bool TodosLosLibrosTienenEstatus()
        {
            return librosCollection.All(p => p.Status != string.Empty);
        }


        public bool AlgunLibroEsDe2005()
        {
            return librosCollection.Any(p => p.PublishedDate.Year == 2005);
        }

        /*----------------------------- Contains -----------------------------------------*/

        public IEnumerable<Book> LibrosCategoriaPython()
        {
            return librosCollection.Where(p => p.Categories.Contains("Python"));
        }

        /*----------------------------- Order -----------------------------------------*/

        public IEnumerable<Book> LibrosJavaOrdenados()
        {
            //return librosCollection.Where(p => p.Categories.Contains("Java")).OrderBy(p=>p.Title).ToList();

            return librosCollection.Where(p => p.Categories.Contains("Java")).OrderByDescending(p => p.Title).ToList();

        }


        public IEnumerable<Book> LibrosOrdenados450Pagina()
        {
            return librosCollection.Where(p=> p.PageCount > 450).OrderByDescending(p=>p.PageCount).ToList();
        }

        public IEnumerable<Book> Primeros3Libros()
        {
            //Elige los ultimos por OrderByDescending
            //return librosCollection.Where(p=> p.Title.Contains("Java"))
            //    .OrderByDescending(p=>p.PageCount)
            //    .Take(3).ToList();

            //Elige los ultimos por el TakeLast
            return librosCollection.Where(p=>p.Title.Contains("Java"))
                .TakeLast(3);
        }


        public IEnumerable<Book> SeleccionaTercerCuarto()
        {
            return librosCollection.Where(p => p.PageCount > 400).Take(4).Skip(2);
        }

        public IEnumerable<Book> AtributosSeleccionados()
        {
            return librosCollection.Take(3).Select(p => new Book(){Title = p.Title, PageCount = p.PageCount});
        }

        public int CantidadLibrosEntre200y500pag()
        {
            return librosCollection.Where(p=> p.PageCount > 200 &&  p.PageCount < 500).Count();
        }


        public DateTime FechaPublicacionMas()
        {
            return librosCollection.Max(p => p.PublishedDate);
        }

        public Book ElementoMenorCantidadPaginas()
        {
            return librosCollection.Where(p => p.PageCount > 0).MinBy(p=> p.PageCount);
        }

        public int SumaTodasLasPaginas()
        {
            return librosCollection.Where(p => p.PageCount > 0 && p.PageCount < 500).Sum(p=> p.PageCount);
        }

        public string TituloLibrosNuevos()
        {
            return librosCollection.Where(p => p.PublishedDate.Year > 2015)
                .Aggregate("", (TitulosLibros, next) =>
                {
                    if (TitulosLibros != string.Empty)
                    {
                        TitulosLibros += " - " + next.Title;
                    }
                    else
                    {
                        TitulosLibros = next.Title;
                    }
                    return TitulosLibros;
                });
        }


        public double ObtenerPromedio()
        {
            return librosCollection.Average(p => p.PageCount);
        }

        public IEnumerable<IGrouping<int, Book>> LibrosDespuesdel2000AgrupadosporAno()
        {
            return librosCollection.Where(p => p.PublishedDate.Year >= 2000).GroupBy(p => p.PublishedDate.Year);
        }


        public ILookup<char, Book> DiccionariosDeLibrosPorLetra()
        {
            return librosCollection.ToLookup(p => p.Title[0], p => p);
        }


        public IEnumerable<Book> LibrosDespuesdel2005conmasde500Pags()
        {
            var LibrosDespuesDel2005 = librosCollection.Where(p => p.PublishedDate.Year > 2005);
            var LirbosConMasDe500Pag = librosCollection.Where(p => p.PageCount > 500);

            return LibrosDespuesDel2005.Join(LibrosDespuesdel2005conmasde500Pags(), p=>p.Title, x=> x.Title, (p,x)=>p);
        }
    }
}
