    using Abp.Domain.Uow;
using CrudAppProject.EmailSender.EmailSenderEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudAppProject.EntityFrameworkCore.Seed.Email
{
    public class EmailTemplateBuilder
    {
        private readonly CrudAppProjectDbContext _context;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly int _tenantId;
        public EmailTemplateBuilder(CrudAppProjectDbContext context, int tenantId)
        {
            _context = context;
            _tenantId = tenantId;
        }
        public void Create()
        {
            string[] templateNames = { "Created", "Delivered", "Dispatched", "Failed" };
            int tenantId = _tenantId;
            foreach (var templateName in templateNames)
            {
                var check = _context.EmailTemplates.FirstOrDefault(t => t.TenantId == _tenantId && t.Name == templateName);
                EmailTemplate newTemplate = null;

                if (check != null) return;

                if (templateName == "Created")
                {
                    newTemplate = new EmailTemplate
                    {
                        TenantId = tenantId,
                        Name = "Created",
                        Subject = "Order Created Successfully!",
                        Content = "<!DOCTYPE html>\r\n" +
                            "<html lang=\"en\">\r\n<head>\r\n" +
                            "<meta charset=\"UTF-8\">\r\n" +
                            "<meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">\r\n" +
                            "<title>Order Confirmation</title>\r\n" +
                            "<style>\r\n" +
                            "body {\r\nfont-family: Arial, sans-serif;\r\n" +
                            "background-color: #f4f4f4;\r\nmargin: 0;\r\npadding: 0;\r\n}\r\n" +
                            ".container {\r\nwidth: 100%;\r\nmax-width: 600px;\r\nmargin: 0 auto;\r\n" +
                            "background-color: #ffffff;\r\npadding: 20px;\r\nborder-radius: 8px;\r\n" +
                            "box-shadow: 0 4px 10px rgba(0, 0, 0, 0.1);\r\n}\r\n" +
                            ".header {\r\nbackground-color: #007BFF;\r\ncolor: #ffffff;\r\n" +
                            "padding: 20px;\r\ntext-align: center;\r\nborder-radius: 8px 8px 0 0;\r\n}\r\n" +
                            ".header h1 {\r\nmargin: 0;\r\n}\r\n" +
                            ".content {\r\npadding: 20px;\r\ncolor: #333333;\r\n}\r\n" +
                            ".content h2 {\r\nfont-size: 24px;\r\ncolor: #333333;\r\n}\r\n" +
                            ".content p {\r\nfont-size: 16px;\r\nline-height: 1.6;\r\n}\r\n" +
                            ".order-details {\r\nmargin-top: 20px;\r\nborder-top: 1px solid #ddd;\r\npadding-top: 10px;\r\n}\r\n" +
                            ".order-details table {\r\nwidth: 100%;\r\nborder-collapse: collapse;\r\n}\r\n" +
                            ".order-details th, .order-details td {\r\npadding: 8px;\r\ntext-align: left;\r\n" +
                            "border-bottom: 1px solid #ddd;\r\n}\r\n" +
                            ".order-details th {\r\nbackground-color: #f8f8f8;\r\n}\r\n" +
                            ".footer {\r\ntext-align: center;\r\npadding: 20px;\r\nbackground-color: #f4f4f4;\r\n" +
                            "border-radius: 0 0 8px 8px;\r\ncolor: #555555;\r\nfont-size: 14px;\r\n}\r\n" +
                            ".footer a {\r\ncolor: #007BFF;\r\ntext-decoration: none;\r\n}\r\n" +
                            ".button {\r\nbackground-color: #007BFF;\r\ncolor: #ffffff;\r\npadding: 10px 20px;\r\n" +
                            "text-align: center;\r\nborder-radius: 5px;\r\n" +
                            "display: inline-block;\r\ntext-decoration: none;\r\n}\r\n" +
                            ".button:hover {\r\nbackground-color: #0056b3;\r\n}\r\n" +
                            "</style>\r\n</head>\r\n<body>\r\n" +
                            "<div class=\"container\">\r\n" +
                            "<div class=\"header\">\r\n<h1>Order Confirmation</h1>\r\n<p>Thank you for your order!</p>\r\n</div>\r\n" +
                            "<div class=\"content\">\r\n<h2>Hello,</h2>\r\n" +
                            "<p>We’re excited to let you know that your order has been successfully confirmed. Below are the details of your order:</p>\r\n" +
                            "<div class=\"order-details\">\r\n" +
                            "<table>\r\n" +
                            "<tr>\r\n<th>Order ID</th>\r\n<td>{{orderNumber}}</td>\r\n</tr>\r\n" +
                            "<tr>\r\n<th>Order Creation Time</th>\r\n<td>{{orderCreationTime}}</td>\r\n</tr>\r\n" +
                            "<tr>\r\n<th>Grand Total</th>\r\n<td>{{grandTotal}}</td>\r\n</tr>\r\n" +
                            "</table>\r\n</div>\r\n" +
                            "<p>If you have any questions or need further assistance, feel free to <a href=\"https://aspnetboilerplate.com/?ref=abptmpl\">contact us</a>.</p>\r\n" +
                            "<p>We appreciate your business and hope to serve you again soon!</p>\r\n</div>\r\n" +
                            "<div class=\"footer\">\r\n<p>&copy; 2024 {{companyName}}. All rights reserved.</p>\r\n" +
                            "<p>Visit our website: <a href=\"{{websiteUrl}}\">www.example.com</a></p>\r\n</div>\r\n" +
                            "</div>\r\n</body>\r\n</html>",
                        Token = "{{customerName}}, {{customerEmail}}, {{orderNumber}}, {{orderCreationTime}}, {{grandTotal}}"
                    };



                }

                if (templateName == "Delivered")
                {
                    newTemplate = new EmailTemplate
                    {
                        TenantId = tenantId,
                        Name = "Delivered",
                        Subject = "Order Delivered Successfully!",
                        Content = "<!DOCTYPE html>\r\n" +
                            "<html lang=\"en\">\r\n<head>\r\n" +
                            "<meta charset=\"UTF-8\">\r\n" +
                            "<meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">\r\n" +
                            "<title>Order Delivered</title>\r\n" +
                            "<style>\r\n" +
                            "body {\r\nfont-family: Arial, sans-serif;\r\n" +
                            "background-color: #f4f4f4;\r\nmargin: 0;\r\npadding: 0;\r\n}\r\n" +
                            ".container {\r\nwidth: 100%;\r\nmax-width: 600px;\r\nmargin: 0 auto;\r\n" +
                            "background-color: #ffffff;\r\npadding: 20px;\r\nborder-radius: 8px;\r\n" +
                            "box-shadow: 0 4px 10px rgba(0, 0, 0, 0.1);\r\n}\r\n" +
                            ".header {\r\nbackground-color: #007BFF;\r\ncolor: #ffffff;\r\n" +
                            "padding: 20px;\r\ntext-align: center;\r\nborder-radius: 8px 8px 0 0;\r\n}\r\n" +
                            ".header h1 {\r\nmargin: 0;\r\n}\r\n" +
                            ".content {\r\npadding: 20px;\r\ncolor: #333333;\r\n}\r\n" +
                            ".content h2 {\r\nfont-size: 24px;\r\ncolor: #333333;\r\n}\r\n" +
                            ".content p {\r\nfont-size: 16px;\r\nline-height: 1.6;\r\n}\r\n" +
                            ".order-details {\r\nmargin-top: 20px;\r\nborder-top: 1px solid #ddd;\r\npadding-top: 10px;\r\n}\r\n" +
                            ".order-details table {\r\nwidth: 100%;\r\nborder-collapse: collapse;\r\n}\r\n" +
                            ".order-details th, .order-details td {\r\npadding: 8px;\r\ntext-align: left;\r\n" +
                            "border-bottom: 1px solid #ddd;\r\n}\r\n" +
                            ".order-details th {\r\nbackground-color: #f8f8f8;\r\n}\r\n" +
                            ".footer {\r\ntext-align: center;\r\npadding: 20px;\r\nbackground-color: #f4f4f4;\r\n" +
                            "border-radius: 0 0 8px 8px;\r\ncolor: #555555;\r\nfont-size: 14px;\r\n}\r\n" +
                            ".footer a {\r\ncolor: #007BFF;\r\ntext-decoration: none;\r\n}\r\n" +
                            ".button {\r\nbackground-color: #007BFF;\r\ncolor: #ffffff;\r\npadding: 10px 20px;\r\n" +
                            "text-align: center;\r\nborder-radius: 5px;\r\n" +
                            "display: inline-block;\r\ntext-decoration: none;\r\n}\r\n" +
                            ".button:hover {\r\nbackground-color: #0056b3;\r\n}\r\n" +
                            "</style>\r\n</head>\r\n<body>\r\n" +
                            "<div class=\"container\">\r\n" +
                            "<div class=\"header\">\r\n<h1>Order Delivered Successfully</h1>\r\n<p>Thank you for your order!</p>\r\n</div>\r\n" +
                            "<div class=\"content\">\r\n<h2>Hello,</h2>\r\n" +
                            "<p>We’re excited to let you know that your order has been successfully Delivered. Below are the details of your order:</p>\r\n" +
                            "<div class=\"order-details\">\r\n" +
                            "<table>\r\n" +
                            "<tr>\r\n<th>Order ID</th>\r\n<td>{{orderNumber}}</td>\r\n</tr>\r\n" +
                            "<tr>\r\n<th>Order Creation Time</th>\r\n<td>{{orderCreationTime}}</td>\r\n</tr>\r\n" +
                            "<tr>\r\n<th>Grand Total</th>\r\n<td>{{grandTotal}}</td>\r\n</tr>\r\n" +
                            "</table>\r\n</div>\r\n" +
                            "<p>If you have any questions or need further assistance, feel free to <a href=\"https://aspnetboilerplate.com/?ref=abptmpl\">contact us</a>.</p>\r\n" +
                            "<p>We appreciate your business and hope to serve you again soon!</p>\r\n</div>\r\n" +
                            "<div class=\"footer\">\r\n<p>&copy; 2024 {{companyName}}. All rights reserved.</p>\r\n" +
                            "<p>Visit our website: <a href=\"{{websiteUrl}}\">www.example.com</a></p>\r\n</div>\r\n" +
                            "</div>\r\n</body>\r\n</html>",
                        Token = "{{customerName}}, {{customerEmail}}, {{orderNumber}}, {{orderCreationTime}}, {{grandTotal}}"
                    };



                }

                if (templateName == "Dispatched")
                {
                    newTemplate = new EmailTemplate
                    {
                        TenantId = tenantId,
                        Name = "Dispatched",
                        Subject = "Order Shipped Successfully!",
                        Content = "<!DOCTYPE html>\r\n " +
                         " <html lang=\"en\">\r\n  <head>\r\n    " +
                         "<meta charset=\"UTF-8\">\r\n " +
                         "<meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">\r\n" +
                         "<title>Order Confirmation</title>\r\n " +
                         "<style>\r\n" +
                         "body {\r\nfont-family: Arial, sans-serif;\r\n" +
                         "background-color: #f4f4f4;\r\n" +
                         "margin: 0;\r\n  " +
                         "padding: 0;\r\n" +
                         "        }\r\n " +
                         ".container {\r\n       " +
                         "     width: 100%;\r\n  " +
                         "     max-width: 600px;\r\n " +
                         "     margin: 0 auto;\r\n  " +
                         "      background-color: #ffffff;\r\n" +
                         "     padding: 20px;\r\n" +
                         "            border-radius: 8px;\r\n" +
                         "            box-shadow: 0 4px 10px rgba(0, 0, 0, 0.1);\r\n" +
                         "        }\r\n" +
                         "        .header {\r\n" +
                         "            background-color: #007BFF;\r\n" +
                         "            color: #ffffff;\r\n            padding: 20px;\r\n            text-align: center;\r\n            border-radius: 8px 8px 0 0;\r\n        }\r\n        .header h1 {\r\n            margin: 0;\r\n        }\r\n        .content {\r\n            padding: 20px;\r\n            color: #333333;\r\n        }\r\n        .content h2 {\r\n            font-size: 24px;\r\n            color: #333333;\r\n        }\r\n        .content p {\r\n            font-size: 16px;\r\n            line-height: 1.6;\r\n        }\r\n        .order-details {\r\n            margin-top: 20px;\r\n            border-top: 1px solid #ddd;\r\n            padding-top: 10px;\r\n        }\r\n        .order-details table {\r\n            width: 100%;\r\n            border-collapse: collapse;\r\n        }\r\n        .order-details th, .order-details td {\r\n            padding: 8px;\r\n            text-align: left;\r\n            border-bottom: 1px solid #ddd;\r\n        }\r\n        .order-details th {\r\n            background-color: #f8f8f8;\r\n        }\r\n        .footer {\r\n            text-align: center;\r\n            padding: 20px;\r\n            background-color: #f4f4f4;\r\n            border-radius: 0 0 8px 8px;\r\n            color: #555555;\r\n            font-size: 14px;\r\n        }\r\n        .footer a {\r\n            color: #007BFF;\r\n            text-decoration: none;\r\n        }\r\n        .button {\r\n            background-color: #007BFF;\r\n            color: #ffffff;\r\n            padding: 10px 20px;\r\n            text-align: center;\r\n            border-radius: 5px;\r\n            display: inline-block;\r\n            text-decoration: none;\r\n        }\r\n        .button:hover {\r\n            background-color: #0056b3;\r\n        }\r\n    </style>\r\n  </head>\r\n  <body>\r\n    <div class=\"container\">\r\n        <div class=\"header\">\r\n            <h1>Order Shipped Successfully !</h1>\r\n            <p>Thank you for your order!</p>\r\n        </div>\r\n        <div class=\"content\">\r\n            <h2>Hello  ,</h2>\r\n            <p>We’re excited to let you know that your order has been successfully Dispatched. Below are the details of your order:</p>\r\n" +
                         " <div class=\"order-details\">\r\n                " +
                         "<table>\r\n                    <tr>\r\n                        <th>Order ID</th>\r\n                        <td>{{orderNumber}}</td>\r\n                    </tr>\r\n                    <tr>\r\n                        <th>Order Creation Time</th>\r\n <td>{{orderCreationTime}}</td>\r\n                    </tr>\r\n                    <tr>\r\n                                           </tr>\r\n                </table>\r\n            </div>\r\n            <p>If you have any questions or need further assistance, feel free to <a href=\"https://aspnetboilerplate.com/?ref=abptmpl\">contact us</a>.</p>\r\n" +
                         "<p>We appreciate your business and hope to serve you again soon!</p>\r\n" +
                         "</div>\r\n        <div class=\"footer\">\r\n            <p>&copy; 2024 {{companyName}}. All rights reserved.</p>\r\n            <p>Visit our website: <a href=\"{{websiteUrl}}\">www.example.com</a></p>\r\n        </div>\r\n    </div>\r\n  </body>\r\n</html>"
 ,

                    };

                }

                if (templateName == "Failed")
                {
                    newTemplate = new EmailTemplate
                    {
                        TenantId = tenantId,
                        Name = "Failed",
                        Subject = "Order Failed!",
                        Content = "<!DOCTYPE html>\r\n " +
                            " <html lang=\"en\">\r\n  <head>\r\n    " +
                            "<meta charset=\"UTF-8\">\r\n " +
                            "<meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">\r\n" +
                            "<title>Order Confirmation</title>\r\n " +
                            "<style>\r\n" +
                            "body {\r\nfont-family: Arial, sans-serif;\r\n" +
                            "background-color: #f4f4f4;\r\n" +
                            "margin: 0;\r\n  " +
                            "padding: 0;\r\n" +
                            "}\r\n " +
                            ".container {\r\n       " +
                            "width: 100%;\r\n  " +
                            "max-width: 600px;\r\n " +
                            "margin: 0 auto;\r\n  " +
                            "background-color: #ffffff;\r\n" +
                            "padding: 20px;\r\n" +
                            " border-radius: 8px;\r\n" +
                            "  box-shadow: 0 4px 10px rgba(0, 0, 0, 0.1);\r\n" +
                            " }\r\n" +
                            " .header {\r\n" +
                            " background-color: #007BFF;\r\n" +
                            " color: #ffffff;\r\n padding: 20px;\r\n " +
                            "text-align: center;\r\n            border-radius: 8px 8px 0 0;\r\n        }\r\n        .header h1 {\r\n            margin: 0;\r\n        }\r\n        .content {\r\n            padding: 20px;\r\n            color: #333333;\r\n        }\r\n        .content h2 {\r\n            font-size: 24px;\r\n            color: #333333;\r\n        }\r\n        .content p {\r\n            font-size: 16px;\r\n            line-height: 1.6;\r\n        }\r\n        .order-details {\r\n            margin-top: 20px;\r\n            border-top: 1px solid #ddd;\r\n            padding-top: 10px;\r\n        }\r\n        .order-details table {\r\n            width: 100%;\r\n            border-collapse: collapse;\r\n        }\r\n        .order-details th, .order-details td {\r\n            padding: 8px;\r\n            text-align: left;\r\n            border-bottom: 1px solid #ddd;\r\n        }\r\n        .order-details th {\r\n            background-color: #f8f8f8;\r\n        }\r\n        .footer {\r\n            text-align: center;\r\n            padding: 20px;\r\n            background-color: #f4f4f4;\r\n            border-radius: 0 0 8px 8px;\r\n            color: #555555;\r\n            font-size: 14px;\r\n        }\r\n        .footer a {\r\n            color: #007BFF;\r\n            text-decoration: none;\r\n        }\r\n        .button {\r\n            background-color: #007BFF;\r\n            color: #ffffff;\r\n            padding: 10px 20px;\r\n            text-align: center;\r\n            border-radius: 5px;\r\n            display: inline-block;\r\n            text-decoration: none;\r\n        }\r\n        .button:hover {\r\n            background-color: #0056b3;\r\n        }\r\n    </style>\r\n  </head>\r\n  <body>\r\n    <div class=\"container\">\r\n        <div class=\"header\">\r\n            <h1>Order Confirmation</h1>\r\n            <p>Thank you for your order!</p>\r\n        </div>\r\n" +
                            " <div class=\"content\">\r\n            <h2>Hello ,</h2>\r\n" +
                            "            <p>There was an issue with your order. Please contact support for assistance. Below are the details of your order:</p>\r\n" +
                            "<div class=\"order-details\">\r\n  " +
                            "<table>\r\n                    <tr>\r\n                        <th>Order ID</th>\r\n                        <td>{{orderNumber}}</td>\r\n                    </tr>\r\n                    <tr>\r\n                        <th>Order Creation Time</th>\r\n <td>{{orderCreationTime}}</td>\r\n                    </tr>\r\n                    <tr>\r\n                                           </tr>\r\n                </table>\r\n            </div>\r\n            <p>If you have any questions or need further assistance, feel free to <a href=\"https://aspnetboilerplate.com/?ref=abptmpl\">contact us</a>.</p>\r\n" +
                            "<p>We appreciate your business and hope to serve you again soon!</p>\r\n" +
                            "</div>\r\n        <div class=\"footer\">\r\n            <p>&copy; 2024 {{companyName}}. All rights reserved.</p>\r\n            <p>Visit our website: <a href=\"{{websiteUrl}}\">www.example.com</a></p>\r\n        </div>\r\n    </div>\r\n  </body>\r\n</html>"
    ,

                    };
                }
                _context.EmailTemplates.Add(newTemplate);

            }

            // If no template exists, create a new one

            _context.SaveChanges();
        }
    }

}

