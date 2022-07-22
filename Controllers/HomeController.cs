using E_Invitation.DTO;
using E_Invitation.Helpers;
using E_Invitation.Models;
using E_Invitation.Repository;
using E_Invitation.Utility;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iText.IO.Font.Constants;
using iText.Kernel.Colors;
using iText.Kernel.Pdf;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Layout;
using iText.Kernel.Pdf.Canvas.Draw;
using Document = iText.Layout.Document;
using iText.IO.Image;
using iText.Kernel.Font;
using iText.IO.Font;
using iText.Kernel.Pdf.Canvas;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;
using System.IO.Compression;
using iText.Kernel.Pdf.Xobject;
using iText.Kernel.Geom;
using Microsoft.AspNetCore.Http;
using iText.Kernel.Pdf.Annot;
using Microsoft.AspNetCore.Authorization;

namespace E_Invitation.Controllers
{
    
    public class HomeController : Controller
    {

        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _oIHostingEnvironment;

        private readonly ILogger<HomeController> _logger;
        public readonly RepositoryOcassion _repositoryOcassion;
        public readonly RepositoryVacancyPlan _repositoryVacancyPlan;
        public readonly RepositoryUser _repositoryUser;
        private readonly IWebHostEnvironment _env;
        public readonly RepositoryGestList _repositoryGestList;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public HomeController(RepositoryOcassion repositoryOcassion, RepositoryVacancyPlan repositoryVacancyPlan, RepositoryUser repositoryUser, IWebHostEnvironment env, RepositoryGestList repositoryGestList, Microsoft.AspNetCore.Hosting.IHostingEnvironment oIHostingEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            _repositoryOcassion = repositoryOcassion;
            _repositoryVacancyPlan = repositoryVacancyPlan;
            _repositoryUser = repositoryUser;
            _env = env;
            _repositoryGestList = repositoryGestList;

            _oIHostingEnvironment = oIHostingEnvironment;
            _httpContextAccessor = httpContextAccessor;
        }

        string username = null;
        string OccassionName = null;


        public IActionResult Index()
        
        {
            User Logins = SessionHelper.GetObjectFromJson<User>(HttpContext.Session, "User");
            if (Logins.IsNotNull())
            {
                ViewBag.AllOcassion = _repositoryOcassion.GetAllActive().Where(c => c.Id != 0);
                // ViewBag.AllUser = _repositoryUser.GetAll(1);


                return View();
            }
            else
                return Redirect("/Login/Index");

        }









        public IActionResult GetAll(int OcassionFilterId)
        {
            User Logins = SessionHelper.GetObjectFromJson<User>(HttpContext.Session, "User");
            if (Logins.IsNotNull())
            {

                var data = _repositoryVacancyPlan.GetAllVacancyUserBy(OcassionFilterId, Logins.Id);
               
                return Json(data);
            }
            else
                return Redirect("/Login/Index");
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public async Task<JsonResult> InvitaionCard1Async(int OcassionId,string Ocassionname)
        {
            string ZipPath = "";
            User Logins = SessionHelper.GetObjectFromJson<User>(HttpContext.Session, "User");

            username = Logins.UserName;
            if (Logins.IsNotNull())
            {
                DTOGestList dTOGestList = new DTOGestList();



                List<DTOGestPass> list = new List<DTOGestPass>();

                var ip = _httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString();


                list = _repositoryGestList.GetAllPass(Logins.Id, OcassionId);

                //DownloadZip();
                string FileName = Ocassionname + "_" + Logins.Id;
                Directory.CreateDirectory(System.IO.Path.Combine(_env.ContentRootPath, "wwwroot/images/Card/" + FileName));

                foreach (DTOGestPass pass in list)
                {
                    string _retFile = GeneratePDF(pass, Ocassionname, Logins.Id, ip);
                }
                ZipPath = System.IO.Path.Combine(_env.ContentRootPath, "wwwroot/images/Card/" + FileName);
                CreateUnprotectedZip(ZipPath);


                // To copy a file to another location and
                // overwrite the destination file if it already exists.
                //  System.IO.File.Move(filePath, Path.Combine(_env.ContentRootPath, "wwwroot/images/Card/" + FileName + "/" + FileName + "1.pdf"), true);
                //System.IO.File.Move(filePath1, Path.Combine(_env.ContentRootPath, "wwwroot/images/Card/" + FileName + "/" + FileName + "2.pdf"), true);

                // ZipPath = Path.Combine(_env.ContentRootPath, "wwwroot/images/Card/" + FileName);
                // CreateUnprotectedZip(ZipPath);

               
                //DownloadZip();

                return Json(FileName);
            }
            else
            {
                return Json(0);
            }

        }


        public IActionResult DownloadZip()
        {
            var memory = DownloadSingleFile("zippedFolder.zip", "wwwroot\\images\\pdf\\");
            return File(memory.ToArray(), "application/pdf", "zippedFolder.zip");
           
        }

        string GeneratePDF(DTOGestPass item, string Ocassionname,int Id,string IPAddress)
        {

            Guid newID = Guid.NewGuid();
            string ZipPath = "";
            string FileName = Ocassionname + "_" + Id;
            try
            {

                StringBuilder text = new StringBuilder();

                

                //Directory.CreateDirectory("wwwroot\\images\\pdf\\" + newID);



                int count = 1;
                var filePath = System.IO.Path.Combine(_env.ContentRootPath, "wwwroot/images/Card/"+FileName+"/" + FileName +"_"+ item.IndlName +"_"+ item.NameOfGest+".pdf");
                PdfWriter writer = new PdfWriter(filePath);
                PdfDocument pdf = new PdfDocument(writer);
                Document document = new Document(pdf);

                ///IP
                ///
                //PdfFont Ipaddfont5 = PdfFontFactory.CreateFont(StandardFontFamilies.HELVETICA);
                //Paragraph Ipadd = new Paragraph("ipaddress : "+IPAddress)
                //.SetTextAlignment(TextAlignment.CENTER).SetFontColor(ColorConstants.BLUE)
                //.SetPaddingBottom(5).SetFontSize(12).SetWordSpacing(3)
                // .SetCharacterSpacing(1).SetFont(Ipaddfont5).SetTextRenderingMode(PdfCanvasConstants.TextRenderingMode.FILL_STROKE)
                //  .SetStrokeWidth(0.5f);

                //document.Add(Ipadd);
                ///////////////////////////
                ///

                Table table22 = new Table(3);
                table22.SetWidth(500);
                Cell Ipcell = new Cell(1, 1)
                       .SetCharacterSpacing(2)
                       .SetPaddingRight(5).SetWidth(200)
                       .SetBorder(iText.Layout.Borders.Border.NO_BORDER).SetHorizontalAlignment(HorizontalAlignment.LEFT)
                       .Add(new Paragraph(IPAddress));
                Cell blankcell = new Cell(1, 1)
                      .SetCharacterSpacing(8)
                      .SetPaddingRight(5).SetWidth(100)
                      .SetBorder(iText.Layout.Borders.Border.NO_BORDER).SetHorizontalAlignment(HorizontalAlignment.CENTER)
                      .Add(new Paragraph(""));

                DateTime date = DateTime.Now;
                Cell timecell = new Cell(1, 1)
                    .SetCharacterSpacing(2)
                    .SetPaddingRight(5).SetWidth(200)
                    .SetBorder(iText.Layout.Borders.Border.NO_BORDER).SetHorizontalAlignment(HorizontalAlignment.RIGHT)
                    .Add(new Paragraph(date.ToString().Replace("-","/")));

                table22.AddCell(Ipcell);
                table22.AddCell(blankcell);
                table22.AddCell(timecell);
                document.Add(table22);



                // .Create(@"wwwroot\\images\\auth\\Army1.jpg"))
                Image img = new Image(ImageDataFactory
                   .Create(@"wwwroot\\images\\ECard\\"+item.Card1))
                   .SetTextAlignment(TextAlignment.CENTER).SetHeight(400).SetWidth(520);

                document.Add(img);


                document.Add(new AreaBreak(AreaBreakType.NEXT_PAGE));
                document.Add(table22);

                // foreach (DTOGestPass item in lst)
                {
                    OccassionName = item.OcassionName;

                    // Must have write permissions to the path folder


                    //string font = PdfFontFactory.CreateFont(FontConstants.HELVETICA_BOLD);

                    //FontProgram fontProgram =
                    //FontProgramFactory.CreateFont(@"C:\Users\lanmanager\Desktop\Font\calibri.ttf");
                    //PdfFont calibri = PdfFontFactory.CreateFont(fontProgram, PdfEncodings.WINANSI);
                    //Font link = FontFactory.GetFont("Arial",);

                    ///////IPaddress////////////////////
                    //document.Add(Ipadd);


                    Style normal = new Style();
                    PdfFont font = PdfFontFactory.CreateFont(StandardFontFamilies.HELVETICA);
                    normal.SetFont(font).SetFontSize(14);
                    Style code = new Style();
                    PdfFont monospace = PdfFontFactory.CreateFont(StandardFontFamilies.HELVETICA);
                    code.SetFont(monospace);

                    // PdfFont font5 = PdfFontFactory.CreateFont("C:/Users/lanmanager/Desktop/Font/Cambria-Font-For-Windows.ttf", PdfEncodings.IDENTITY_H);
                     PdfFont font5 = PdfFontFactory.CreateFont(StandardFontFamilies.HELVETICA);

                    Paragraph header = new Paragraph(item.ChiefName)
                   .SetTextAlignment(TextAlignment.CENTER)
                   .SetFontColor(ColorConstants.RED)

                    .SetPaddingBottom(10)
                     .SetFontSize(22)
                     .SetFont(font5)
                            .SetTextRenderingMode(PdfCanvasConstants.TextRenderingMode.FILL_STROKE)
                            .SetStrokeWidth(0.5f);

                    document.Add(header);
                  //  PdfFont font6 = PdfFontFactory.CreateFont("C:/Users/lanmanager/Desktop/Font/nk_monotype.ttf", PdfEncodings.IDENTITY_H);

                    PdfFont font6 = PdfFontFactory.CreateFont(StandardFontFamilies.HELVETICA);

                    Paragraph subheader = new Paragraph("")
                 .SetTextAlignment(TextAlignment.CENTER)
                 .SetFontScript(iText.Commons.Utils.UnicodeScript.OLD_ITALIC)
                 .SetFontColor(ColorConstants.RED)
                 .SetFontSize(18);
                    document.Add(subheader);

                    Paragraph header1 = new Paragraph("Chief of the Army Staff")
                    .SetTextAlignment(TextAlignment.CENTER)
                     .SetFontScript(iText.Commons.Utils.UnicodeScript.OLD_ITALIC)
                    .SetFontColor(ColorConstants.BLUE)
                    .SetPaddingBottom(-6)

                     .SetFontSize(22)
                    .SetWordSpacing(3)
                    .SetCharacterSpacing(1)
                      .SetFont(font6)
                            .SetTextRenderingMode(PdfCanvasConstants.TextRenderingMode.FILL_STROKE)
                            .SetStrokeWidth(0.5f);

                    document.Add(header1);

                    Paragraph subheader1 = new Paragraph("and All Ranks of the Army")
                    .SetTextAlignment(TextAlignment.CENTER)

                    .SetFontColor(ColorConstants.BLUE)
                    .SetPaddingBottom(-10)
                    .SetFontSize(16)
                    .SetWordSpacing(3)
                    .SetCharacterSpacing(1)
                      .SetFont(font6)
                            .SetTextRenderingMode(PdfCanvasConstants.TextRenderingMode.FILL_STROKE)
                            .SetStrokeWidth(0.5f);




                    document.Add(subheader1);


                    Paragraph subheader2 = new Paragraph("request the company of")
                    .SetTextAlignment(TextAlignment.CENTER)
                    .SetFontColor(ColorConstants.BLUE)
                    .SetPaddingBottom(5)

                    .SetFontSize(16)
                    .SetWordSpacing(3)
                    .SetCharacterSpacing(1)
                      .SetFont(font6)
                            .SetTextRenderingMode(PdfCanvasConstants.TextRenderingMode.FILL_STROKE)
                            .SetStrokeWidth(0.5f);

                    document.Add(subheader2);

                    Paragraph subheader5 = new Paragraph("at the \u00A0" + item.NameOfGest)
                   .SetTextAlignment(TextAlignment.CENTER)
                   .SetFontColor(ColorConstants.BLUE)
                   .SetPaddingBottom(5)
                     .SetFontSize(16)
                    .SetWordSpacing(3)
                    .SetCharacterSpacing(1)
                      .SetFont(font6)
                            .SetTextRenderingMode(PdfCanvasConstants.TextRenderingMode.FILL_STROKE)
                            .SetStrokeWidth(0.5f);

                    document.Add(subheader5);


                    LineSeparator ls = new LineSeparator(new SolidLine())

                         .SetFontColor(ColorConstants.BLUE);
                    document.Add(ls);


                    Paragraph subheader3 = new Paragraph(item.OcassionName)
                    .SetTextAlignment(TextAlignment.CENTER)
                    .SetFontColor(ColorConstants.BLUE)

                    .SetFontSize(22)
                    .SetWordSpacing(3)
                    .SetCharacterSpacing(1)
                      .SetFont(font6)
                            .SetTextRenderingMode(PdfCanvasConstants.TextRenderingMode.FILL_STROKE)
                            .SetStrokeWidth(0.5f);



                    document.Add(subheader3);

                   

                    Table table = new Table(3, false);

                    table.SetBorderCollapse(BorderCollapsePropertyValue.SEPARATE);
                    table.SetVerticalBorderSpacing(10);
                    table.SetHorizontalBorderSpacing(10);

                    Cell cell11 = new Cell(1, 1)

                       .Add(new Paragraph(item.ContactName + "\n"
                       + item.IssueBranch + "\n" + "Tele:" + item.PhoneNo + "\n" +
                       "ASCON:" + item.ASCON))
                      .SetBorder(iText.Layout.Borders.Border.NO_BORDER)
                       .SetFontColor(ColorConstants.BLUE)
                       .SetMarginRight(35)

                        .SetFontSize(12)
                    .SetWordSpacing(3)
                    .SetCharacterSpacing(1)
                      .SetFont(font6)
                            .SetTextRenderingMode(PdfCanvasConstants.TextRenderingMode.FILL_STROKE)
                            .SetStrokeWidth(0.5f);

                    Cell cell12 = new Cell(1, 1)
                        .SetCharacterSpacing(15)
                        .SetPaddingRight(25)
                        .SetBorder(iText.Layout.Borders.Border.NO_BORDER)
                        .Add(new Paragraph(""));


                    Cell cell13 = new Cell(1, 1)


                       .Add(new Paragraph("Venue : " + "\u00A0" + item.Venue + "\n" +
                      "Time   : " + "\u00A0" + item.Time + "\n" +
                      "Dress  : " + "\u00A0Service Officers:" + item.Dress + "\t\u00A0\u00A0\u00A0\u00A0\u00A0\u00A0\u00A0\u00A0     Civilian Officers:" + item.Dress1 + "\t\n\u00A0\u00A0\u00A0\u00A0\u00A0\u00A0\u00A0\u00A0  " +
                     "    (Medals may be worn by Veterans)\n"))
                       .SetFontColor(ColorConstants.BLUE)

                       .SetBorder(iText.Layout.Borders.Border.NO_BORDER)
                       .SetMarginLeft(19)

                      .SetFont(PdfFontFactory.CreateFont(StandardFontFamilies.HELVETICA))

                        .SetFontSize(16)

                      .SetFont(font6)
                            .SetTextRenderingMode(PdfCanvasConstants.TextRenderingMode.FILL_STROKE)
                            .SetStrokeWidth(0.5f);


                    table.AddCell(cell11);

                    table.AddCell(cell12);

                    table.AddCell(cell13);

                    document.Add(table);

                    Paragraph subheader6 = new Paragraph("Please bring this card along. Do not carry any Handbags and Cameras.")
                     .SetTextAlignment(TextAlignment.CENTER)
                     .SetFontColor(ColorConstants.BLUE)

                       .SetFontSize(14)
                    .SetWordSpacing(3)
                    .SetCharacterSpacing(1)
                      .SetFont(font6)
                            .SetTextRenderingMode(PdfCanvasConstants.TextRenderingMode.FILL_STROKE)
                            .SetStrokeWidth(0.5f);

                    document.Add(subheader6);


                    //String IMAGE1 = @"wwwroot\\images\\watermarks.png";
                    //Image imgd = GetWatermarkedImage(pdf, new Image(ImageDataFactory.Create(IMAGE1)));
                    //document.Add(imgd);


                    //Image srcImage = new Image(ImageDataFactory.Create(IMAGE1));
                    //srcImage.ScaleToFit(400, 700);
                    //Image imgd = GetWatermarkedImage(pdf, srcImage);
                    //document.Add(imgd);




                    //Image img22 = new Image(ImageDataFactory
                    //.Create(@"wwwroot\\images\\Pcar.png"))
                    //.SetTextAlignment(TextAlignment.CENTER).SetFontColor(color);
                    //document.Add(img22);

                    document.Add(new AreaBreak(AreaBreakType.NEXT_PAGE));


                    ///////IPaddress////////////////////
                    ///
                  
                    document.Add(table22);


                    Image img2 = new Image(ImageDataFactory
                  .Create(@"wwwroot\\images\\ECard\\" + item.Card2))
                   .SetTextAlignment(TextAlignment.CENTER).SetHeight(400).SetWidth(520);
                    document.Add(img2);

                    document.Add(new AreaBreak(AreaBreakType.NEXT_PAGE));
                    ///////IPaddress////////////////////
                    
                    document.Add(table22);


                    Image img3 = new Image(ImageDataFactory
                    .Create(@"wwwroot\\images\\ECard\\" + item.Card3))
                    .SetTextAlignment(TextAlignment.CENTER).SetHeight(400).SetWidth(520);
                    document.Add(img3);




                    document.Add(new AreaBreak(AreaBreakType.NEXT_PAGE));
                    /////IPaddress/////////////
                   

                    document.Add(table22);

                    //Div div1 = new Div();
                    //div1.SetProperty(Property.OVERFLOW_Y, OverflowPropertyValue.HIDDEN);
                    //div1.Add(new Paragraph("Veh Parking"));
                    //div1.SetHeight(50);
                    //div1.SetWidth(200);
                    //div1.SetFontSize(25);

                    //div1.SetTextAlignment(TextAlignment.CENTER);
                    //Color color1 = WebColors.GetRGBColor(item.EnclosureColor); // Color name to RGB
                    //div1.SetHorizontalAlignment(HorizontalAlignment.CENTER);
                    //div1.SetFontColor(ColorConstants.BLUE);
                    //document.Add(div1);


                    Div div = new Div();
                    div.SetProperty(Property.OVERFLOW_Y, OverflowPropertyValue.HIDDEN);
                    div.Add(new Image(ImageDataFactory
                    .Create(@"wwwroot\\images\\Ashokchakar.png"))
                    .SetTextAlignment(TextAlignment.CENTER).SetHeight(50).SetHorizontalAlignment(HorizontalAlignment.CENTER));

                    div.Add(new Paragraph("\n" + item.OcassionName).SetHorizontalAlignment(HorizontalAlignment.CENTER).SetFontColor(ColorConstants.WHITE).SetFontSize(12).SetTextAlignment(TextAlignment.CENTER));
                    div.Add(new Paragraph(item.OcassionDate.ToShortDateString().ToString().Replace("-", "/")).SetHorizontalAlignment(HorizontalAlignment.CENTER).SetFontColor(ColorConstants.WHITE).SetFontSize(12).SetTextAlignment(TextAlignment.CENTER));
                    div.Add(new Paragraph("VEHICLE PASS : ENCL-"+item.EnclosureName).SetHorizontalAlignment(HorizontalAlignment.CENTER).SetFontColor(ColorConstants.WHITE).SetFontSize(12).SetTextAlignment(TextAlignment.CENTER));
                    div.Add(new Paragraph("ID").SetHorizontalAlignment(HorizontalAlignment.CENTER).SetFontColor(ColorConstants.WHITE).SetFontSize(12).SetTextAlignment(TextAlignment.CENTER));
                    div.Add(new Paragraph("xxxx-xxxx-xxxx-"+ item.Adharno.Substring((item.Adharno.Length - 4), 4)).SetHorizontalAlignment(HorizontalAlignment.CENTER).SetFontColor(ColorConstants.WHITE).SetFontSize(12).SetTextAlignment(TextAlignment.CENTER));

                    div.SetHeight(200);
                    div.SetWidth(200).SetMarginTop(200);
                    Color color = WebColors.GetRGBColor(item.EnclosureColor); // Color name to RGB
                    div.SetBackgroundColor(color);
                    div.SetHorizontalAlignment(HorizontalAlignment.CENTER).SetPadding(10).SetBorderRadius(new BorderRadius(200));
                    div.SetFontColor(ColorConstants.RED);
                   
                    document.Add(div);

                    count++;


                }
                document.Close();
                writer.Close();
                pdf.Close();
               // var filePath1 = Path.Combine(_env.ContentRootPath, "wwwroot/images/Card/" + FileName + "_2.pdf");
               // PdfWriter writer1 = new PdfWriter(filePath1);

               // PdfDocument pdf1 = new PdfDocument(writer1);
               // Document document1 = new Document(pdf1);

               // Image img = new Image(ImageDataFactory
               // .Create(@"wwwroot\\images\\auth\\Army1.jpg"))
               // .SetTextAlignment(TextAlignment.CENTER);
               // document1.Add(img);

               // Image img2 = new Image(ImageDataFactory
               //.Create(@"wwwroot\\images\\auth\\Army2.jpg"))
               //.SetTextAlignment(TextAlignment.CENTER);
               // document1.Add(img2);


               // Image img3 = new Image(ImageDataFactory
               // .Create(@"wwwroot\\images\\auth\\Army3.jpg"))
               // .SetTextAlignment(TextAlignment.CENTER);
               // document1.Add(img3);

               // document1.Close();
               // writer1.Close();
               // pdf1.Close();

                //PdfWriter writer3 = new PdfWriter("wwwroot\\images\\Card\\" + newID + "4.pdf");

                //PdfDocument pdf3 = new PdfDocument(writer3);
                //Document document3 = new Document(pdf3);



                //Image img3 = new Image(ImageDataFactory
                //.Create(@"wwwroot\\images\\auth\\Army3.jpg"))
                //.SetTextAlignment(TextAlignment.CENTER);
                //document3.Add(img3);
                //document3.Close();




                //PdfWriter writer2 = new PdfWriter("wwwroot\\images\\Card\\" + newID + "3.pdf");
                //PdfDocument pdf2 = new PdfDocument(writer2);
                //Document document2 = new Document(pdf2);



                //Image img2 = new Image(ImageDataFactory
                //.Create(@"wwwroot\\images\\auth\\Army2.jpg"))
                //.SetTextAlignment(TextAlignment.CENTER);
                //document2.Add(img2);
                //document2.Close();




              //  string Source = @"C:\Users\lanmanager\Desktop\E-InvitationSql\E-Invitation\wwwroot\images\Card\";
                //string Destin = @"C:\Users\lanmanager\Desktop\E-InvitationSql\E-Invitation\wwwroot\images\Card\";

                //string Destin = @"C:\Users\lanmanager\Desktop\E-InvitationSql\E-Invitation\wwwroot\images\pdf\MyDir1\";

                //string[] files = Directory.GetFiles(filePath);

               // if (System.IO.File.Exists(Source + "/" + FileName+ "_1.pdf"))
                //{
                    // To copy a folder's contents to a new location:
                    // Create a new target folder.
                    // If the directory already exists, this method does not create a new directory.
              //Directory.CreateDirectory(Path.Combine(_env.ContentRootPath, "wwwroot/images/Card/" + FileName));



               
              //  // To copy a file to another location and
              //  // overwrite the destination file if it already exists.
              //  System.IO.File.Move(filePath, Path.Combine(_env.ContentRootPath, "wwwroot/images/Card/" + FileName+"/"+FileName+"1.pdf"), true);
              //  //System.IO.File.Move(filePath1, Path.Combine(_env.ContentRootPath, "wwwroot/images/Card/" + FileName + "/" + FileName + "2.pdf"), true);

              //  ZipPath = Path.Combine(_env.ContentRootPath, "wwwroot/images/Card/" + FileName);
              //  CreateUnprotectedZip(ZipPath);
                // }

                //foreach (String fil in files)
                //{
                //    string filename = Path.GetFileName(fil);
                //    System.IO.File.Move(fil, Destin + filename, true);
                //}


                //string sourceFolder = @"C:\Users\lanmanager\Desktop\E-InvitationSql\E-Invitation\wwwroot\images\pdf\MyDir1";
                //string targetZipFile = @"C:\Users\lanmanager\Desktop\E-InvitationSql\E-Invitation\wwwroot\images\pdf\"+ username + OccassionName +"_"+ newID +".zip";


                //System.IO.Compression.ZipFile.CreateFromDirectory(sourceFolder, targetZipFile);

            }

            catch (Exception ex)
            {

            }
            return FileName;

        }
        private static Image GetWatermarkedImage(PdfDocument pdfDocument, Image img)
        {
            float width = img.GetImageScaledWidth();
            float height = img.GetImageScaledHeight();
            PdfFormXObject template = new PdfFormXObject(new Rectangle(width, height));

            // Use a highlevel Canvas to add the image because the scaling properties were set to the
            // highlevel Image object.
            new Canvas(template, pdfDocument)
                .Add(img)
                .Close();

            //new PdfCanvas(template, pdfDocument)
            //    .SaveState()
            //    .SetStrokeColor(ColorConstants.GREEN)
            //    .SetLineWidth(3)
            //    .MoveTo(width * 0.25f, height * 0.25f)
            //    .LineTo(width * 0.75f, height * 0.75f)
            //    .MoveTo(width * 0.25f, height * 0.75f)
            //    .LineTo(width * 0.25f, height * 0.25f)
            //    .Stroke()
            //    .SetStrokeColor(ColorConstants.WHITE)
            //    .Ellipse(0, 0, width, height)
            //    .Stroke()
            //    .RestoreState();

            return new Image(template);
        }
        void CreateUnprotectedZip(string foldername)
        {
          
            try
            {
                if (Directory.Exists(foldername))
                {
                    //TO create a zip file.  
                    if (System.IO.File.Exists(foldername+".zip"))
                        System.IO.File.Delete(foldername + ".zip");

                    System.IO.Compression.ZipFile.CreateFromDirectory(foldername, foldername+".zip");
                }
            }
            catch (Exception ex)
            {
                
            }
        }


        private MemoryStream DownloadSingleFile(string filename, string uploadPath)
        {


            var path = System.IO.Path.Combine(Directory.GetCurrentDirectory(), uploadPath, filename);

            var memory = new MemoryStream();
            if (System.IO.File.Exists(path))
            {
                var net = new System.Net.WebClient();
                var data = net.DownloadData(path);
                var content = new System.IO.MemoryStream(data);
                memory = content;
            }
            memory.Position = 0;
            return memory;
        }


      

    }
}


