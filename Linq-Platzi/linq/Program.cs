using System.Linq;

using linq;

Console.WriteLine("Hello, World!");


LinqQueries queries = new LinqQueries();

/*-------------Where-----------------------*/


//Libros despues del 2000
//var despues2000 = queries.LibrosDespesdel2000();



//Libros mayor a 250 paginas
//var mas250paginas = queries.LibrosMas250Paginas();
//ImprimirValores(mas250paginas);


/*--------------------All & Any -------------------*/

//Console.WriteLine(queries.TodosLosLibrosTienenEstatus());

//Console.WriteLine($" Alguno libro se publico el 2005? - {queries.AlgunLibroEsDe2005()}");


/*--------------------Contains---------------------*/
//ImprimirValores(queries.LibrosCategoriaPython());



/*------------OrderBy----------------------------*/
//ImprimirValores(queries.LibrosJavaOrdenados());

//ImprimirValores(queries.LibrosOrdenados450Pagina());



/*--------------Take--------------------------*/

//ImprimirValores(queries.Primeros3Libros());


/*-----------Count---------------------*/
//Console.WriteLine(queries.CantidadLibrosEntre200y500pag());


/*----------------Max & Min-------------------------*/
//Console.WriteLine(queries.FechaPublicacionMas());

/*-----------------MaxBy & MinBy----------------*/
//var elemento = queries.ElementoMenorCantidadPaginas();
//Console.WriteLine($"El libro {elemento.Title} tiene {elemento.PageCount} paginas");



/*------------------------Sum & Aggregate------------------*/
//Console.WriteLine($"La suma de paginas es {queries.SumaTodasLasPaginas()}");


/*----------------------------Imprimir grupo-------------------------------*/
//ImprimirGrupo(queries.LibrosDespuesdel2000AgrupadosporAno());


/*----------------------------Imprimir grupo-------------------------------*/
var diccionario = queries.DiccionariosDeLibrosPorLetra();
ImprimirDiccionario(diccionario, 'A');


void ImprimirValores(IEnumerable<Book> listadelibros)
{
    Console.WriteLine("{0,-60} {1, 15} {2, 15}\n", "Titulo", "N. Paginas", "Fecha publicacion");
    foreach (var item in listadelibros)
    {
        Console.WriteLine("{0,-60} {1, 15} {2, 15}", item.Title, item.PageCount, item.PublishedDate.ToShortDateString());
    }
}


void ImprimirGrupo(IEnumerable<IGrouping<int, Book>> ListadeLibros)
{
    foreach (var grupo in ListadeLibros)
    {
        Console.WriteLine("");
        Console.WriteLine($"Grupo: {grupo.Key}");
        Console.WriteLine("{0,-60} {1, 15} {2, 15}\n", "Titulo", "N. Paginas", "Fecha publicacion");
        foreach (var item in grupo)
        {
            Console.WriteLine("{0,-60} {1, 15} {2, 15}", item.Title, item.PageCount, item.PublishedDate.Date.ToShortDateString());
        }
    }
}


void ImprimirDiccionario(ILookup<char, Book> ListadeLibros, char letra)
{
    Console.WriteLine("{0,-60} {1, 15} {2, 15}\n", "Titulo", "N. Paginas", "Fecha publicacion");
    foreach (var item in ListadeLibros[letra])
    {
        Console.WriteLine("{0,-60} {1, 15} {2, 15}", item.Title, item.PageCount, item.PublishedDate.Date.ToShortDateString());
    }
}