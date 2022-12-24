using Newtonsoft.Json;

namespace MovieDB.Test
{
    //public class MovieTest
    //{
    //    private readonly CustomEndPointTest _customEndPointTest;

    //    public MovieTest()
    //    {
    //        //Inject dependencies to run server work
    //        _customEndPointTest = new CustomEndPointTest(x =>
    //        {
    //            x.AddAutoMapperConfig();
    //            x.AddRepositoryInjections();
    //            x.AddServicesInjections();
    //        });
    //    }

    //    [Fact]
    //    public async void GetMoviesTest()
    //    {
    //        using var httpClient = _customEndPointTest.CreateClient();
    //        var response = await httpClient.GetAsync("/api/Movies/GetMovies");

    //        var responseText = await response.Content.ReadAsStringAsync();
    //        var content = JsonConvert.DeserializeObject<PageListDTO<MovieDTO>>(responseText);

    //        Assert.Equal(10, content.ResultList.Count);
    //    }

    //    [Fact]
    //    public async void GetMoviesByIdTest()
    //    {
    //        string movieId = "b6eefefe-4636-4e9d-f376-08dacdc18dd3";
    //        using var httpClient = _customEndPointTest.CreateClient();

    //        var response = await httpClient.GetAsync($"/api/Movies/GetMovieById/{movieId}");
    //        var responseText = await response.Content.ReadAsStringAsync();
    //        var content = JsonConvert.DeserializeObject<MovieDTO>(responseText);

    //        Assert.Equal("Lorem Ipsum is simply dummy text of the printing and typesetting", content.Sinopsis);
    //    }

    //    [Fact]
    //    public async void DelMoviesTest()
    //    {
    //        string movieId = "b6eefefe-4636-4e9d-f376-08dacdc18dd3";
    //        using var httpClient = _customEndPointTest.CreateClient();

    //        var response = await httpClient.DeleteAsync($"/api/Movies/DelMovies?muvieId={movieId}");
    //        var responseText = await response.Content.ReadAsStringAsync();
    //        var content = JsonConvert.DeserializeObject<ResponseModel>(responseText);

    //        Assert.Equal("Registro eliminado exitosamente", content.Message);
    //    }

    //    [Fact]
    //    public async void PutMoviesTest()
    //    {
    //        using var httpClient = _customEndPointTest.CreateClient();

    //        var newMovie = new Dictionary<string, dynamic>
    //        {
    //            { "MovieId", "ac09c499-57a5-4296-f377-08dacdc18dd3" }, //Conditional to edit
    //            { "Title", "Updated Movie" },
    //            { "Sinopsis", "Movie Updated Test sinpsis" },
    //            { "GenreId", "ed3ad8ba-6054-426c-9926-08dacdc18d33" },
    //            { "Duration", "180" },
    //            { "RelasedDate", DateTime.Now },
    //        };

    //        var reqParams = JsonConvert.SerializeObject(newMovie);
    //        var response = await httpClient.PutAsync("/api/Movies/PutMovies", new StringContent(reqParams, System.Text.Encoding.UTF8, "application/json"));

    //        var responseText = await response.Content.ReadAsStringAsync();
    //        var content = JsonConvert.DeserializeObject<ResponseModel<MovieDTO>>(responseText);

    //        Assert.Equal("Updated Movie", content.Result.Title);
    //    }

    //    [Fact]
    //    public async void PostMoviesTest()
    //    {
    //        using var httpClient = _customEndPointTest.CreateClient();

    //        var newMovie = new Dictionary<string, dynamic>
    //        {
    //            { "Title", "Movie Test" },
    //            { "Sinopsis", "Movie Test sinpsis" },
    //            { "GenreId", "ed3ad8ba-6054-426c-9926-08dacdc18d33" },
    //            { "Duration", "120" },
    //            { "RelasedDate", DateTime.Now },
    //        };

    //        var reqParams = JsonConvert.SerializeObject(newMovie);
    //        var response = await httpClient.PostAsync("/api/Movies/PostMovie", new StringContent(reqParams, System.Text.Encoding.UTF8, "application/json"));

    //        var responseText = await response.Content.ReadAsStringAsync();
    //        var content = JsonConvert.DeserializeObject<ResponseModel<MovieDTO>>(responseText);

    //        Assert.Equal("Movie Test sinpsis", content.Result.Sinopsis);
    //    }


    //    //Ratings test
    //    [Fact]
    //    public async void PostRatingTest()
    //    {
    //        using var httpClient = _customEndPointTest.CreateClient();

    //        var newMovie = new Dictionary<string, dynamic>
    //        {
    //              { "moviesId", "2c0409e0-be45-44a0-f37d-08dacdc18dd3" },
    //              { "ratingPoint", 1 },
    //              { "title", "Bad movie" },
    //              { "comment", "Wrong !!" },
    //              { "userName", "dperez" }
    //        };

    //        var reqParams = JsonConvert.SerializeObject(newMovie);
    //        var response = await httpClient.PostAsync("/api/Rating/PostRatings", new StringContent(reqParams, System.Text.Encoding.UTF8, "application/json"));

    //        var responseText = await response.Content.ReadAsStringAsync();
    //        var content = JsonConvert.DeserializeObject<ResponseModel<RatingDTO>>(responseText);

    //        Assert.Equal("dperez", content.Result.UserName);
    //    }

    //    [Fact]
    //    public async void GetRatingByMovieIdTest()
    //    {
    //        string movieId = "d23d2459-12f4-4608-f378-08dacdc18dd3";
    //        using var httpClient = _customEndPointTest.CreateClient();

    //        var response = await httpClient.GetAsync($"/api/Rating/GetRatingsByMovie?movieId={movieId}");
    //        var responseText = await response.Content.ReadAsStringAsync();
    //        var content = JsonConvert.DeserializeObject<IList<RatingDTO>>(responseText);

    //        Assert.NotNull(content);
    //    }
    //}

    //Config to run backend code in background while run test and call endpoint directly
    //internal class CustomEndPointTest : WebApplicationFactory<Program>
    //{
    //    private readonly Action<IServiceCollection> _serviceCollection;

    //    public CustomEndPointTest(Action<IServiceCollection> serviceCollection)
    //    {
    //        _serviceCollection = serviceCollection;
    //    }

    //    protected override IHost CreateHost(IHostBuilder builder)
    //    {
    //        builder.ConfigureServices(_serviceCollection);
    //        return base.CreateHost(builder);
    //    }
    //}
}