 
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Net.Http.Headers;
using System.Text.Json;
using DragonballAPI.Models;
using static PlanetsListVm;
using DragonBallZ.Models.DTOs;

namespace DragonBallZ.Controllers
{
    public class DragonBallPlanets : Controller
    {

        private string API_DRAGONBALL = "https://dragonball-api.com/api/";



        public async Task<IActionResult> Index(int page = 1, int limit = 10, string? name = null, bool? isDestroyed = null, int? partial = null)
        {
            // QS
            var qs = new Dictionary<string, string?>
            {
                ["page"] = page.ToString(),
                ["limit"] = limit.ToString()
            };
            if (!string.IsNullOrWhiteSpace(name)) qs["name"] = name;
            if (isDestroyed.HasValue) qs["isDestroyed"] = isDestroyed.Value.ToString().ToLowerInvariant();

            var url = QueryHelpers.AddQueryString($"{API_DRAGONBALL}planets", qs);

            using var http = new HttpClient();
            http.DefaultRequestHeaders.Accept.ParseAdd("application/json");

            var resp = await http.GetAsync(url);
            var raw = await resp.Content.ReadAsStringAsync();

            if (!resp.IsSuccessStatusCode || string.IsNullOrWhiteSpace(raw))
                return View(new PlanetsListVm { Page = page, Limit = limit, Name = name, IsDestroyed = isDestroyed });

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            PlanetsApiResponse? api = null;
            List<Planets>? list = null;

            try
            {
                var firstChar = raw.TrimStart()[0];
                if (firstChar == '{')
                {
                    // Caso sin filtros: { items, meta, links }
                    api = JsonSerializer.Deserialize<PlanetsApiResponse>(raw, options);
                }
                else if (firstChar == '[')
                {
                    // Caso con filtros: [ ... ] (sin meta/links)
                    list = JsonSerializer.Deserialize<List<Planets>>(raw, options);
                }
                else
                {
                    return View(new PlanetsListVm { Page = page, Limit = limit, Name = name, IsDestroyed = isDestroyed });
                }
            }
            catch (JsonException)
            {
                return View(new PlanetsListVm { Page = page, Limit = limit, Name = name, IsDestroyed = isDestroyed });
            }

            // Normaliza
            var items = api?.Items ?? list ?? new List<Planets>();

            // Si vino array (sin meta), crea una meta mínima solo para la navegación
            var meta = api?.Meta ?? new Meta
            {
                TotalItems = items.Count,         // desconocido globalmente; se usa conteo de página
                ItemCount = items.Count,
                ItemsPerPage = limit,
                TotalPages = (items.Count == limit) ? page + 1 : page, // “quizá hay más” si llena la página
                CurrentPage = page
            };

            var vm = new PlanetsListVm
            {
                Items = items,
                Meta = meta,
                Links = api?.Links,    // null si vino array
                Page = page,
                Limit = limit,
                Name = name,
                IsDestroyed = isDestroyed
            };

            if (partial == 1)
                return PartialView("~/Views/DragonBallPlanets/_IndexContent.cshtml", vm);

            return View(vm);
        }



        // GET: DragonBallPlanets/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id < 1)
                return BadRequest("Id inválido.");

            var url = $"{API_DRAGONBALL}planets/{id}";

            using var http = new HttpClient();
            http.DefaultRequestHeaders.Accept.ParseAdd("application/json");

            var resp = await http.GetAsync(url);
            var raw = await resp.Content.ReadAsStringAsync();

            if (!resp.IsSuccessStatusCode || string.IsNullOrWhiteSpace(raw))
                return View(new PlanetDetailsVm { Id = id });

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            PlanetDetail? dto;
            try
            {
                dto = JsonSerializer.Deserialize<PlanetDetail>(raw, options);
                if (dto is null) return View(new PlanetDetailsVm { Id = id });
            }
            catch (JsonException)
            {
                return View(new PlanetDetailsVm { Id = id });
            }

            var vm = new PlanetDetailsVm
            {
                Id = dto.Id,
                Name = dto.Name ?? "(sin nombre)",
                IsDestroyed = dto.IsDestroyed,
                Description = dto.Description,
                Image = dto.Image,
                Characters = (dto.Characters ?? new List<CharacterSummary>())
                    .Where(c => c.DeletedAt is null)
                    .ToList()
            };

            return View(vm); // renderiza Views/DragonBallPlanets/Details.cshtml
        }

        // GET: DragonBallPlanets/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DragonBallPlanets/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction( );
            }
            catch
            {
                return View();
            }
        }

        // GET: DragonBallPlanets/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DragonBallPlanets/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction( );
            }
            catch
            {
                return View();
            }
        }

        // GET: DragonBallPlanets/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DragonBallPlanets/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction( );
            }
            catch
            {
                return View();
            }
        }
    }
}
