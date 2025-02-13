using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ResourcesWebApplication.Models.Accounting;
using ResourcesWebApplication.Models.Context;
using System.Diagnostics;
using System.IO;
using System.Collections.Immutable;

namespace ResourcesWebApplication.Controllers
{
    public class PlanComptableMarocainController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PlanComptableMarocainController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> ExerciseNamesDistinct()
        {
            try
            {
                var names = await _context.Figures.Select(d => d.NoExercice).Distinct().ToListAsync();
                var noCompteMontant = await _context.Enancees.Where(d => d.NoExercice == names[0].ToString()).ToListAsync();
                if(names[0] != noCompteMontant[0].NoExercice)
                {
                    return NoContent();
                }                
                return Ok(names);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        public IActionResult GenerateExercise_1_1(string name, string identifier)
        {
            try
            {
                if (string.IsNullOrEmpty(name))
                {
                    return NotFound();
                }
                switch (identifier)
                {
                    case "FigureFicheDelInventaireExtraComptable":                        
                        List<Figure> figureFicheDelInventaireExtraComptable = FigureFicheDelInventaireExtraComptable(name);
                        return Ok(figureFicheDelInventaireExtraComptable);
                    case "FicheDelInventaireExtraComptable":
                        List<NoComptesMontant> ficheDelInventaireExtraComptable = FicheDelInventaireExtraComptable();
                        return Ok(ficheDelInventaireExtraComptable);
                    case "FigureExtraitDelaBalanceAvantInventaire":
                        List<Figure> figureExtraitDelaBalanceAvantInventaire = FigureExtraitDelaBalanceAvantInventaire(name);
                        return Ok(figureExtraitDelaBalanceAvantInventaire);
                    case "ExtraitDelaBalanceAvantInventaire":
                        List<NoComptesMontant> extraitDelaBalanceAvantInventaire = ExtraitDelaBalanceAvantInventaire();
                        return Ok(extraitDelaBalanceAvantInventaire);
                    case "GetEnanceeByExercise":
                        List<Enancee> getEnanceeByExercise = GetEnanceeByExercise(name);
                        return Ok(getEnanceeByExercise);
                    case "TravauxAFaire":
                        List<TravailAFaire> travauxAFaire = TravauxAFaire(name);
                        return Ok(travauxAFaire);
                    default:
                        break;
                }
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        public List<TravailAFaire> TravauxAFaire(string name)
        {
            var travail = _context.TravauxAFaire.Where(d => d.NoExercice == $"{name}").Select(d => d.Instruction).ToList();
            List<TravailAFaire> objectList = new List<TravailAFaire>();
            foreach (var item in travail)
            {
                var objectData = new TravailAFaire()
                {
                    Instruction = item.Replace('_', ' '),
                };
                objectList.Add(objectData);
            }
            return objectList;
        }
        public List<Figure> FigureFicheDelInventaireExtraComptable(string name)
        {
            var figure = _context.Figures.Where(d => d.NoExercice == $"{name}" && d.Title.Contains("Fiche de l'inventaire".Replace(' ', '_'))).Select(d => d.Title).ToList();
            List<Figure> objectList = new List<Figure>();
            foreach (var item in figure)
            {
                var objectData = new Figure()
                {
                    Title = item.Replace('_', ' '),
                };
                objectList.Add(objectData);
            }
            return objectList;
        }
        public List<NoComptesMontant> FicheDelInventaireExtraComptable()
        {
            float[] montants = {60500, 153250};
            var nombres = _context.PlanComptableMarocain.Where(d => d.No_Compte == 3121 || d.No_Compte == 3151).Select(d => d.No_Compte).ToList();
            var comptes = _context.PlanComptableMarocain.Where(d => d.No_Compte == 3121 || d.No_Compte == 3151).Select(d => d.Libelle).ToList();
            

            List<NoComptesMontant> data = new List<NoComptesMontant>();
            for (int i = 0; i < nombres.Count; i++)
            {
                var comptesMontant = new NoComptesMontant()
                {
                    Nombre = nombres[i].ToString(),
                    Compte = comptes[i].ToString(),
                    Montant = montants[i].ToString()
                };
                data.Add(comptesMontant);
            }
            return data;
        }
        public List<Figure> FigureExtraitDelaBalanceAvantInventaire(string name)
        {
            var figure = _context.Figures.Where(d => d.NoExercice == $"{name}" && d.Title.Contains("Extrait de la balance".Replace(' ', '_'))).Select(d => d.Title).ToList();
            List<Figure> objectList = new List<Figure>();
            foreach (var item in figure)
            {
                var objectData = new Figure()
                {
                    Title = item.Replace('_', ' '),
                };
                objectList.Add(objectData);
            }
            return objectList;
        }
        public List<NoComptesMontant> ExtraitDelaBalanceAvantInventaire()
        {
            float[] montants = {45500, 163750};
            var nombres = _context.PlanComptableMarocain.Where(d => d.No_Compte == 3121 || d.No_Compte == 3151).Select(d => d.No_Compte).ToList();
            var comptes = _context.PlanComptableMarocain.Where(d => d.No_Compte == 3121 || d.No_Compte == 3151).Select(d => d.Libelle).ToList();
            
            List<NoComptesMontant> data = new List<NoComptesMontant>();
            for (int i = 0; i < nombres.Count; i++)
            {
                var comptesMontant = new NoComptesMontant()
                {
                    Nombre = nombres[i].ToString(),
                    Compte = comptes[i].ToString(),
                    Montant = montants[i].ToString()
                };
                data.Add(comptesMontant);
            }
            return data;
        }
        public List<Enancee> GetEnanceeByExercise(string name)
        {
            var enancee = _context.Enancees.Where(d => d.NoExercice == $"{name}" && d.Information.Contains("L'entreprise TAZI vous fournit".Replace(' ', '_'))).Select(d => d.Information).ToList();
            List<Enancee> objectList = new List<Enancee>();
            foreach (var item in enancee)
            {
                var objectData = new Enancee()
                {
                    Information = item.Replace('_', ' '),
                };
                objectList.Add(objectData);
            }
            return objectList;
        }
        [HttpPost]
        // [Route("EnanceePost")]
        public async Task<IActionResult> EnanceePost([FromBody] Enancee enancee)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return NoContent();
                }
                _context.Enancees.Add(enancee);
                await _context.SaveChangesAsync();             
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        // [Route("EnanceeRunPost")]
        public async Task<IActionResult> EnanceeRunPost(string noExercise, string information)
        {
            try
            {  
                if (string.IsNullOrWhiteSpace(noExercise) || string.IsNullOrWhiteSpace(information))
                {
                    return NotFound();
                }
                using (Process process = new Process())
                {
                    ProcessStartInfo startInfo = new ProcessStartInfo();
                    startInfo.FileName = "cmd.exe";
                    startInfo.RedirectStandardInput = true;
                    startInfo.UseShellExecute = false;

                    process.StartInfo = startInfo;
                    process.Start();

                    StreamWriter sw = process.StandardInput;
                    if (sw.BaseStream.CanWrite)
                    {
                        sw.WriteLine(@"cd C:\Users\dell\Entrepreneurship\Engineering\Accounting\DataScience");
                        sw.WriteLine(@"C:\Users\dell\Entrepreneurship\Engineering\Accounting\DataScience\data_science\Scripts\activate");
                        sw.WriteLine(@"python.exe C:\Users\dell\Entrepreneurship\Engineering\Accounting\DataScience\Enancee\post.py" + " " + noExercise.Replace(' ', '_') + " " + information.Replace(' ', '_'));
                        sw.WriteLine(@"deactivate");
                        sw.Close();
                    }
                    process.WaitForExit();
                }    
                
                await Task.Delay(4);

                var enancees = await _context.Enancees.Take(2).OrderByDescending(d => d.Id).ToListAsync();  
                
                if (enancees.Count == 0)
                {
                    return NoContent();
                }

                List<Enancee> enanceeList = new List<Enancee>(); 
                foreach (var item in enancees)
                {
                    Enancee data = new Enancee()
                    {
                        Id = item.Id,
                        NoExercice = item.NoExercice.Replace('_', ' '),
                        Information = item.Information.Replace('_', ' ')
                    };
                    enanceeList.Add(data);
                }     
                
                return Ok(enanceeList);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetEnancees()
        {
            try
            {
                var enancees = await _context.Enancees.Take(4).OrderByDescending(d => d.Id).ToListAsync();        
                if (enancees.Count == 0)
                {
                    return NoContent();
                }
                List<Enancee> enanceeList = new List<Enancee>(); 
                foreach (var item in enancees)
                {
                    Enancee data = new Enancee()
                    {
                        Id = item.Id,
                        NoExercice = item.NoExercice.Replace('_', ' '),
                        Information = item.Information.Replace('_', ' ')
                    };
                    enanceeList.Add(data);
                }     
                
                return Ok(enanceeList);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        // [Route("TravailAFairePost")]
        public async Task<IActionResult> TravailAFairePost([FromBody] TravailAFaire travailAFaire)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return NoContent();
                }
                _context.TravauxAFaire.Add(travailAFaire);
                await _context.SaveChangesAsync();             
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        // [Route("FigureRunPost")]
        public async Task<IActionResult> TravailAFaireRunPost(string noExercise, string instuction)
        {
            try
            {  
                if (string.IsNullOrWhiteSpace(noExercise) || string.IsNullOrWhiteSpace(instuction))
                {
                    return NotFound();
                }
                using (Process process = new Process())
                {
                    ProcessStartInfo startInfo = new ProcessStartInfo();
                    startInfo.FileName = "cmd.exe";
                    startInfo.RedirectStandardInput = true;
                    startInfo.UseShellExecute = false;

                    process.StartInfo = startInfo;
                    process.Start();

                    StreamWriter sw = process.StandardInput;
                    if (sw.BaseStream.CanWrite)
                    {
                        sw.WriteLine(@"cd C:\Users\dell\Entrepreneurship\Engineering\Accounting\DataScience");
                        sw.WriteLine(@"C:\Users\dell\Entrepreneurship\Engineering\Accounting\DataScience\data_science\Scripts\activate");
                        sw.WriteLine(@"python.exe C:\Users\dell\Entrepreneurship\Engineering\Accounting\DataScience\Travail\post.py" + " " + noExercise.Replace(' ', '_') + " " + instuction.Replace(' ', '_'));
                        sw.WriteLine(@"deactivate");
                        sw.Close();
                    }
                    process.WaitForExit();
                }    
                
                await Task.Delay(4);

                var travail = await _context.TravauxAFaire.Take(2).OrderByDescending(d => d.Id).ToListAsync();  
                
                if (travail.Count == 0)
                {
                    return NoContent();
                }

                List<TravailAFaire> travailList = new List<TravailAFaire>(); 
                foreach (var item in travail)
                {
                    TravailAFaire data = new TravailAFaire()
                    {
                        Id = item.Id,
                        NoExercice = item.NoExercice.Replace('_', ' '),
                        Instruction = item.Instruction.Replace('_', ' ')
                    };
                    travailList.Add(data);
                }     
                
                return Ok(travailList);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetTravaux()
        {
            try
            {
                var travaux = await _context.TravauxAFaire.Take(4).OrderByDescending(d => d.Id).ToListAsync();        
                if (travaux.Count == 0)
                {
                    return NoContent();
                }
                List<TravailAFaire> travauxList = new List<TravailAFaire>(); 
                foreach (var item in travaux)
                {
                    TravailAFaire data = new TravailAFaire()
                    {
                        Id = item.Id,
                        NoExercice = item.NoExercice.Replace('_', ' '),
                        Instruction = item.Instruction.Replace('_', ' ')
                    };
                    travauxList.Add(data);
                }     
                
                return Ok(travauxList);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        public IActionResult FigureInjection()
        {
            return View();
        }
        [HttpPost]
        // [Route("FigurePost")]
        public async Task<IActionResult> FigurePost([FromBody] Figure figure)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return NoContent();
                }
                _context.Figures.Add(figure);
                await _context.SaveChangesAsync();             
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetFigures()
        {
            try
            {
                var figures = await _context.Figures.Take(4).OrderByDescending(d => d.Id).ToListAsync();        
                if (figures.Count == 0)
                {
                    return NoContent();
                }
                List<Figure> figuresList = new List<Figure>(); 
                foreach (var item in figures)
                {
                    Figure figure = new Figure()
                    {
                        Id = item.Id,
                        NoExercice = item.NoExercice.Replace('_', ' '),
                        Title = item.Title.Replace('_', ' ')
                    };
                    figuresList.Add(figure);
                }     
                
                return Ok(figuresList);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpGet]
        // [Route("FigureRunPost")]
        public async Task<IActionResult> FigureRunPost(string noExercise, string title)
        {
            try
            {  
                if (string.IsNullOrWhiteSpace(noExercise) || string.IsNullOrWhiteSpace(title))
                {
                    return NotFound();
                }
                using (Process process = new Process())
                {
                    ProcessStartInfo startInfo = new ProcessStartInfo();
                    startInfo.FileName = "cmd.exe";
                    startInfo.RedirectStandardInput = true;
                    startInfo.UseShellExecute = false;

                    process.StartInfo = startInfo;
                    process.Start();

                    StreamWriter sw = process.StandardInput;
                    if (sw.BaseStream.CanWrite)
                    {
                        sw.WriteLine(@"cd C:\Users\dell\Entrepreneurship\Engineering\Accounting\DataScience");
                        sw.WriteLine(@"C:\Users\dell\Entrepreneurship\Engineering\Accounting\DataScience\data_science\Scripts\activate");
                        sw.WriteLine(@"python.exe C:\Users\dell\Entrepreneurship\Engineering\Accounting\DataScience\Figures\post.py" + " " + noExercise.Replace(' ', '_') + " " + title.Replace(' ', '_'));
                        sw.WriteLine(@"deactivate");
                        sw.Close();
                    }
                    process.WaitForExit();
                }    
                
                await Task.Delay(4);

                var figures = await _context.Figures.Take(14).OrderByDescending(d => d.Id).ToListAsync();  
                
                if (figures.Count == 0)
                {
                    return NoContent();
                }

                List<Figure> figuresList = new List<Figure>(); 
                foreach (var item in figures)
                {
                    Figure figure = new Figure()
                    {
                        Id = item.Id,
                        NoExercice = item.NoExercice.Replace('_', ' '),
                        Title = item.Title.Replace('_', ' ')
                    };
                    figuresList.Add(figure);
                }     
                
                return Ok(figuresList);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        // [Route("PlanComptablePost")]
        public async Task<IActionResult> PlanComptablePost([FromBody] PlanComptable planComptable)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return NoContent();
                }
                _context.PlanComptableMarocain.Add(planComptable);
                await _context.SaveChangesAsync();                
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        // [Route("GetPCGM")]
        public async Task<IActionResult> GetPCGM()
        {
            try
            {
                return Ok(await _context.PlanComptableMarocain.ToListAsync());
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        // GET: PlanComptableMarocain/IndexFigure
        // public async Task<IActionResult> FigureIndex()
        // {
        //     return View(await _context.Figures.Take(10).OrderByDescending(d => d.Id).ToListAsync());
        // }

        // GET: PlanComptableMarocain/DetailsEnancee/5
        public async Task<IActionResult> DetailsEnancee(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enancee = await _context.Enancees
                .FirstOrDefaultAsync(m => m.Id == id);
            if (enancee == null)
            {
                return NotFound();
            }
            Enancee enanceeData = new Enancee()
            {
                NoExercice = enancee.NoExercice.Replace('_', ' '),
                Information = enancee.Information.Replace('_', ' ')
            };
                
            return View(enanceeData);
        }
        // GET: PlanComptableMarocain/DetailsTravailAFaire/5
        public async Task<IActionResult> DetailsTravailAFaire(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var travail = await _context.TravauxAFaire
                .FirstOrDefaultAsync(m => m.Id == id);
            if (travail == null)
            {
                return NotFound();
            }
            TravailAFaire travailData = new TravailAFaire()
            {
                NoExercice = travail.NoExercice.Replace('_', ' '),
                Instruction = travail.Instruction.Replace('_', ' ')
            };
                
            return View(travailData);
        }
        // GET: PlanComptableMarocain/DetailsFigure/5
        public async Task<IActionResult> DetailsFigure(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var figure = await _context.Figures
                .FirstOrDefaultAsync(m => m.Id == id);
            if (figure == null)
            {
                return NotFound();
            }
            Figure figureData = new Figure()
            {
                NoExercice = figure.NoExercice.Replace('_', ' '),
                Title = figure.Title.Replace('_', ' ')
            };
                
            return View(figureData);
        }
        // GET: PlanComptableMarocain
        public async Task<IActionResult> Index()
        {
            return View(await _context.PlanComptableMarocain.Take(10).OrderByDescending(d => d.Id).ToListAsync());
        }

        // GET: PlanComptableMarocain/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var planComptable = await _context.PlanComptableMarocain
                .FirstOrDefaultAsync(m => m.Id == id);
            if (planComptable == null)
            {
                return NotFound();
            }

            return View(planComptable);
        }

        // GET: PlanComptableMarocain/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PlanComptableMarocain/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Classe,Niv_de_Reg,No_Compte,Libelle")] PlanComptable planComptable)
        {
            if (ModelState.IsValid)
            {
                _context.Add(planComptable);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(planComptable);
        }
        // GET: PlanComptableMarocain/EditEnancee/5
        public async Task<IActionResult> EditEnancee(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enancee = await _context.Enancees.FindAsync(id);
            if (enancee == null)
            {
                return NotFound();
            }
            Enancee enanceeData = new Enancee()
            {
                NoExercice = enancee.NoExercice.Replace('_', ' '),
                Information = enancee.Information.Replace('_', ' ')
            };
                
            return View(enanceeData);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditEnancee(int id, [Bind("Id,NoExercice,Information")] Enancee enancee)
        {
            if (id != enancee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(enancee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EnanceeExists(enancee.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(enancee);
        }
        // GET: PlanComptableMarocain/EditTravailAFaire/5
        public async Task<IActionResult> EditTravailAFaire(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var travail = await _context.TravauxAFaire.FindAsync(id);
            if (travail == null)
            {
                return NotFound();
            }
            TravailAFaire travailData = new TravailAFaire()
            {
                NoExercice = travail.NoExercice.Replace('_', ' '),
                Instruction = travail.Instruction.Replace('_', ' ')
            };
                
            return View(travailData);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditTravailAFaire(int id, [Bind("Id,NoExercice,Instruction")] TravailAFaire travail)
        {
            if (id != travail.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(travail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TravailAFaireExists(travail.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(travail);
        }

        // GET: PlanComptableMarocain/EditFigure/5
        public async Task<IActionResult> EditFigure(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var figure = await _context.Figures.FindAsync(id);
            if (figure == null)
            {
                return NotFound();
            }
            Figure figureData = new Figure()
            {
                NoExercice = figure.NoExercice.Replace('_', ' '),
                Title = figure.Title.Replace('_', ' ')
            };
                
            return View(figureData);
        }

        // POST: PlanComptableMarocain/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditFigure(int id, [Bind("Id,NoExercice,Title")] Figure figure)
        {
            if (id != figure.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(figure);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FigureExists(figure.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(figure);
        }
        // GET: PlanComptableMarocain/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var planComptable = await _context.PlanComptableMarocain.FindAsync(id);
            if (planComptable == null)
            {
                return NotFound();
            }
            return View(planComptable);
        }

        // POST: PlanComptableMarocain/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Classe,Niv_de_Reg,No_Compte,Libelle")] PlanComptable planComptable)
        {
            if (id != planComptable.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(planComptable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlanComptableExists(planComptable.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(planComptable);
        }

        // GET: PlanComptableMarocain/DeleteEnancee/5
        public async Task<IActionResult> DeleteEnancee(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enancee = await _context.Enancees
                .FirstOrDefaultAsync(m => m.Id == id);
            if (enancee == null)
            {
                return NotFound();
            }

            return View(enancee);
        }

        // GET: PlanComptableMarocain/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var planComptable = await _context.PlanComptableMarocain
                .FirstOrDefaultAsync(m => m.Id == id);
            if (planComptable == null)
            {
                return NotFound();
            }

            return View(planComptable);
        }

        // GET: PlanComptableMarocain/DeleteFigure/5
        public async Task<IActionResult> DeleteFigure(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var figures = await _context.Figures
                .FirstOrDefaultAsync(m => m.Id == id);
            if (figures == null)
            {
                return NotFound();
            }
            Figure figureData = new Figure()
            {
                NoExercice = figures.NoExercice.Replace('_', ' '),
                Title = figures.Title.Replace('_', ' ')
            };
                
            return View(figureData);
        }

        // GET: PlanComptableMarocain/DeleteTravailAFaire/5
        public async Task<IActionResult> DeleteTravailAFaire(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var travail = await _context.TravauxAFaire
                .FirstOrDefaultAsync(m => m.Id == id);
            if (travail == null)
            {
                return NotFound();
            }
            TravailAFaire travailData = new TravailAFaire()
            {
                NoExercice = travail.NoExercice.Replace('_', ' '),
                Instruction = travail.Instruction.Replace('_', ' ')
            };
                
            return View(travailData);
        }

        // POST: PlanComptableMarocain/DeleteEnancee/5
        [HttpPost, ActionName("DeleteEnancee")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteEnanceeConfirmed(int id)
        {
            var enancee = await _context.Enancees.FindAsync(id);
            _context.Enancees.Remove(enancee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        // POST: PlanComptableMarocain/DeleteTravailAFaire/5
        [HttpPost, ActionName("DeleteTravailAFaire")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteTravailAFaireConfirmed(int id)
        {
            var travail = await _context.TravauxAFaire.FindAsync(id);
            _context.TravauxAFaire.Remove(travail);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        // POST: PlanComptableMarocain/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var planComptable = await _context.PlanComptableMarocain.FindAsync(id);
            _context.PlanComptableMarocain.Remove(planComptable);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // POST: PlanComptableMarocain/DeleteFigure/5
        [HttpPost, ActionName("DeleteFigure")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteFigureConfirmed(int id)
        {
            var figure = await _context.Figures.FindAsync(id);
            _context.Figures.Remove(figure);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlanComptableExists(int id)
        {
            return _context.PlanComptableMarocain.Any(e => e.Id == id);
        }
        private bool FigureExists(int id)
        {
            return _context.Figures.Any(e => e.Id == id);
        }
        // TravailAFaireExists
        private bool TravailAFaireExists(int id)
        {
            return _context.TravauxAFaire.Any(e => e.Id == id);
        }
        private bool EnanceeExists(int id)
        {
            return _context.Enancees.Any(e => e.Id == id);
        }
    }
}
