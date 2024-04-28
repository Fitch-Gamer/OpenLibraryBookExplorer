using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Reflection;
using System.Drawing;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OpenLibraryBookExplorer
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchFuncController : ControllerBase
    {
        

        public class PostResponse
        {
            public int numFound;
            public int start;
            public bool numFoundExact;
            public List<BookInfo> docs;
            

        }

        public class SearchModel
        {
            public string Search { get; set; }
        }

        public class BookInfo
        {
            //public byte[] Cover;
            

            //public List<string> author_alternative_name;
            //public List<string> author_key;
            public List<string> author_name;
            //public List<string> contributor;
            public string cover_edition_key;
            //public int cover_i;
            //public List<string> ddc;
            //public string eboook_access;
            //public int ebook_count_i;
            //public int edition_count;
            //public List<string> edition_key;
            //public int first_publish_year;
            //public string first_sentence;
            //public List<string> format;
            //public bool has_fulltext;
            //public List<string> ia;
            //public List<string> ia_collection;
            //public string ia_collection_s;
            //public List<string> isbn;
            //public string key;
            //public List<string> language;
            //public int last_modified_i;
            //public List<string> lcc;
            //public List<string> lccn;
            //public string lending_edition_s;
            //public string lending_identifier_s;
            //public int number_of_paged_median;
            //public List<string> oclc;
            //public int osp_count;
            //public string printdisabled_s;
            //public bool public_scan_b;
            //public List<string> publish_date;
            //public List<string> publish_place;
            //public List<string> publish_year;
            //public List<string> publisher;
            //public List<string> seed;
            public string title;
            //public string title_suggest;
            //public string title_sort;
            //public string type;
            //public List<string> id_librarything;
            //public List<string> id_goodreads;
            //public List<string> id_amazon;
            //public List<string> id_depósito_legal;
            //public List<string> id_alibris_id;
            //public List<string> id_google;
            //public List<string> id_paperback_swap;
            //public List<string> id_wikidata;
            //public List<string> id_overdrive;
            //public List<string> id_canadian_national_library_archive;
            //public List<string> subject;
            //public List<string> place;
            //public List<string> time;
            //public List<string> person;
            //public List<string> ia_loaded_id;
            //public List<string> ia_box_id;
            //public float ratings_average;
            //public float ratings_sortable;
            //public int ratings_count;
            //public int ratings_count_1;
            //public int ratings_count_2;
            //public int ratings_count_3;
            //public int ratings_count_4;
            //public int ratings_count_5;
            //public int readinglog_count;
            //public int want_to_read_count;
            //public int currently_reading_count;
            //public int already_read_count;
            //public List<string> publisher_facet;
            //public List<string> person_key;
            //public List<string> time_facet;
            //public List<string> place_key;
            //public List<string> person_facet;
            //public List<string> subject_facet;
            //public float _version_;
            //public List<string> place_facet;
            //public string lcc_sort;
            //public List<string> author_facet;
            //public List<string> subject_key;
            //public float ddc_sort;
            //public List<string> time_key;


            public string Cover;

        }

        private readonly ILogger<SearchFuncController> _logger;
        public SearchFuncController(ILogger<SearchFuncController> logger)
        {
            _logger = logger;
        }

        // POST api/<SearchFuncController>
        [HttpPost]
        public IActionResult Post([FromBody] SearchModel model)
        {
            _logger.LogInformation("recieved");

            //Task<PostResponse> ReturnedApi =  GetBooks(model.Search);

            PostResponse BooksReturned = Task.Run(() => GetBooks(model.Search)).Result;
             //= ReturnedApi.Result;
            _logger.LogInformation(BooksReturned.ToString());

            return new JsonResult(BooksReturned);
        }

        public async Task<PostResponse> GetBooks(string Params)
        {
            HttpClient Client = new HttpClient();
            Client.BaseAddress = new Uri("https://openlibrary.org/search.json");
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string parameters = $"?q={Params}";

            HttpResponseMessage response = await Client.GetAsync(parameters).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                string jsonString = await response.Content.ReadAsStringAsync();
                //_logger.LogInformation(jsonString);
                PostResponse Books = JsonConvert.DeserializeObject<PostResponse>(jsonString);
                /*
                HttpClient ImageClient = new HttpClient();
                ImageClient.BaseAddress = new Uri("https://covers.openlibrary.org/b/olid/");
                ImageClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("image/jpeg"));
                

                foreach (BookInfo b in Books.docs)
                {
                    string CoverParam = $"{b.cover_edition_key}-M.jpg";
                    HttpResponseMessage ImageResponse = await ImageClient.GetAsync(CoverParam).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                    {
                        b.Cover = await ImageResponse.Content.ReadAsByteArrayAsync();
                    }
                }
                */
                int i = 0;

                foreach (BookInfo b in Books.docs)
                {
                    Books.docs[i].Cover = "https://covers.openlibrary.org/b/olid/" + Books.docs[i].cover_edition_key + "-M.jpg";
                    i++;
                }

                return Books;
            }


            return null;
        }
    }

}
