using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Markup;
using System.Windows.Media;
///<summary>
/// Created by Created by Clive Leddy
/// clive.leddy@gmail.com
/// Date 2019-02-06
/// </summary>
namespace InvoiceMaker.ViewModel.Util
{
    class PrintInvoiceDialogBox
    {
        /// <summary>
        /// Add company invoice and dates.
        /// </summary>
        /// <param name="document">GenerateInvoiceDocument the formatted components of the invoice.</param>
        /// <returns>TextBlock</returns>
        private static UIElement SetTitle(GenerateInvoiceDocument document)
        {
            TextBlock title = document.GetHeadder();
            Grid.SetColumn(title, 2);
            Grid.SetRow(title, 0);
            Grid.SetColumnSpan(title, 2);

            return title;
        }

        /// <summary>
        /// Add receiver address.
        /// </summary>
        /// <param name="document">GenerateInvoiceDocument the formatted components of the invoice.</param>
        /// <returns>TextBlock</returns>
        private static UIElement SetReceiverAddress(GenerateInvoiceDocument document)
        {
            TextBlock receiveCompanyAddress = document.GetReceiverAddress();
            Grid.SetColumn(receiveCompanyAddress, 2);
            Grid.SetRow(receiveCompanyAddress, 1);
            Grid.SetColumnSpan(receiveCompanyAddress, 2);

            return receiveCompanyAddress;
        }

        /// <summary>
        /// Add the invoice items.
        /// </summary>
        /// <param name="document">GenerateInvoiceDocument the formatted components of the invoice.</param>
        /// <returns>RichTextBlock</returns>
        private static UIElement SetInvoiceItems(GenerateInvoiceDocument document)
        {
            //Get the invoice items
            RichTextBox richTextBox = new RichTextBox();
            richTextBox.FontFamily = new FontFamily("Times Courier");
            richTextBox.FontSize = 16;
            richTextBox.HorizontalAlignment = HorizontalAlignment.Stretch;
            richTextBox.IsReadOnly = true;
            richTextBox.BorderBrush = new SolidColorBrush();
            Paragraph p = new Paragraph(
                new Run(document.GetInvoiceItems())
                );
            p.BorderThickness = new Thickness(1, 1, 1, 1);
            p.BorderBrush = new SolidColorBrush();
            richTextBox.Document.Blocks.Add(p
               );
            Grid.SetColumn(richTextBox, 0);
            Grid.SetRow(richTextBox, 2);
            Grid.SetColumnSpan(richTextBox, 4);
            // Grid.SetRowSpan(richTextBox, 2);
            return richTextBox;
        }

        /// <summary>
        /// Add the calculated data for amount to pay, discount, total, and total tax.
        /// </summary>
        /// <param name="document">GenerateInvoiceDocument the formatted components of the invoice.</param>
        /// <returns>Grid</returns>
        private static UIElement SetTotalTaxTotalAmount(GenerateInvoiceDocument document)
        {
            Grid invoiceTotals = document.GetInvoiceTotalAmountAndTax();
            Grid.SetColumn(invoiceTotals, 2);
            Grid.SetRow(invoiceTotals, 3);
            Grid.SetColumnSpan(invoiceTotals, 2);

            return invoiceTotals;
        }
        /// <summary>
        /// Add the company address.
        /// </summary>
        /// <param name="document">GenerateInvoiceDocument the formatted components of the invoice.</param>
        /// <returns>TextBlock</returns>
        private static UIElement SetCompanyAddress(GenerateInvoiceDocument document)
        {

            TextBlock companyAddress = document.GetCompanyAddress();
            Grid.SetColumn(companyAddress, 0);
            Grid.SetRow(companyAddress, 3);
            Grid.SetColumnSpan(companyAddress, 2);
            return companyAddress;
        }

        /// <summary>
        /// Add company contact info
        /// </summary>
        /// <param name="document">GenerateInvoiceDocument the formatted components of the invoice.</param>
        /// <returns>TextBlock</returns>
        private static UIElement SetCompanyContactInfo(GenerateInvoiceDocument document)
        {
            TextBlock companyContactInforamtion = document.GetSenderContactInfo();
            Grid.SetColumn(companyContactInforamtion, 1);
            Grid.SetRow(companyContactInforamtion, 3);
            Grid.SetColumnSpan(companyContactInforamtion, 2);
            return companyContactInforamtion;
        }
        /// <summary>
        /// Adjust the rows of the grid.
        /// </summary>
        /// <param name="documentGrid">Grid</param>
        private static void AdjustGridRows(Grid documentGrid)
        {
            int i = 0;
            foreach (var item in documentGrid.RowDefinitions)
            {
                if (i == 2)
                {
                    item.Height = new GridLength(500);

                }
                else if (i == 3)
                {
                    item.Height = new GridLength(150);
                }
                else
                {
                    item.Height = new GridLength(120);
                }
                i++;
            }
        }

        /// <summary>
        /// Populate the grid with elements.
        /// </summary>
        /// <param name="document">GenerateInvoiceDocument invoice data</param>
        /// <returns>Grid</returns>
        private static Grid PopulateDocumentGrid(GenerateInvoiceDocument document)
        {
            Grid documentGrid = document.CreateAGrid(4, 4, 900, 700);
            AdjustGridRows(documentGrid);//adjust the row of the grid

            //add invoice id and dates
            documentGrid.Children.Add(
                SetTitle(document));

            //add receiver address
            documentGrid.Children.Add(
            SetCompanyName(document));

            //add receiver address
            documentGrid.Children.Add(
                SetReceiverAddress(document));

            //add invoice items
            documentGrid.Children.Add(
                SetInvoiceItems(document));

            //add the invoice total amounts and total tax
            documentGrid.Children.Add(
                SetTotalTaxTotalAmount(document));

            //add company address
            documentGrid.Children.Add(
                SetCompanyAddress(document));

            //add company contact info
            documentGrid.Children.Add(
                SetCompanyContactInfo(document));

            return documentGrid;
        }

        private static UIElement SetCompanyName(GenerateInvoiceDocument document)
        {
            UIElement blk = document.GetCompanyName();
            Grid.SetRow(blk, 1);
            Grid.SetColumn(blk, 0);
            return blk;
        }

        /// <summary>
        /// Print the invoice
        /// </summary>
        /// <param name="document">GenerateInvoiceDocument the formatted components of the invoice.</param>
        public static void PrintInvoiceDialog(GenerateInvoiceDocument document)
        {
            //initialize a new PrintDialog object to run the print job
            PrintDialog pd = new PrintDialog();

            PageContent pc;
            FixedDocument doc;
            FixedPage fxpg;
            Border border;
            Grid documentGrid;

            //check that the show PrintDialog returns true then print the data
            if (pd.ShowDialog() == true)
            {
                //initialize a FixedDocument object
                //this is a document that may contain one or more pages
                doc = new FixedDocument();
                
                //assign size properties for the overall document, 
                //get the width and height (also know as paper size) from the PrintDialog
                doc.DocumentPaginator.PageSize = new Size(pd.PrintableAreaWidth, pd.PrintableAreaHeight);
    
                //initialize the FixedPage object
                //add individual page(s) to the overall document
                fxpg = new FixedPage();
                //set the FixedPage width and height with sizes from the FixedDocument
                fxpg.Width = doc.DocumentPaginator.PageSize.Width;
                fxpg.Height = doc.DocumentPaginator.PageSize.Height;
                
                //create a grid for the document
                documentGrid = PopulateDocumentGrid(document);
       
                //add the document grid to the border
                //create a new border
                border = new Border();
                border.BorderThickness = new Thickness(2, 2, 2, 2);
                border.BorderBrush = new SolidColorBrush(Colors.LightSlateGray);
                border.Child = documentGrid;
                border.HorizontalAlignment = HorizontalAlignment.Center;                
                //add the border that frames the grid               
                fxpg.Children.Add(border);


                //add page content to the FixedDocument
                //Initialize a PageContent object
                pc = new PageContent();
                
                //add the FixedPage to the PageContent
                ((IAddChild)pc).AddChild(fxpg);
                //add the PageContent to the FixedDocument
                doc.Pages.Add(pc);

                //send FixedDocument to the printer
                pd.PrintDocument(doc.DocumentPaginator, "Printing invoice: " + document.GetInvoiceId());
            }
        }
    }
}
