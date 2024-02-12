﻿using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using PurchaseOrderWebApplication.Models;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using Humanizer;
using Microsoft.Extensions.Hosting;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Authorization;

namespace PurchaseOrderWebApplication.Controllers
{
    public class PurchaseOrderController : Controller
    {
        string connectionString = "Server=192.168.0.23,1427;Initial Catalog=interns;Integrated Security=False;user id=Interns;password=test;Connection Timeout=10;";
        List<PurchaseOrder> PurchaseList = new List<PurchaseOrder>();

        [HttpGet]
        [Authorize]
        public IActionResult Index()
        {
            return  View();
        }

        [OutputCache(Duration = 30)]
        [Route("View")]
        [Authorize]
        public async Task<IActionResult> viewOrder()
        {
            try
            {
                var displayTable = "select * from PurchaseOrder";
                using (IDbConnection connection = new SqlConnection(connectionString))
                {
                    PurchaseList = (await connection.QueryAsync<PurchaseOrder>(displayTable)).ToList();
                }
            }
            catch (Exception e) { Console.WriteLine(e.Message); }
            return await Task.Run(() => View(PurchaseList)); 
        }
        
        [HttpGet]
        [Route("Create")]
        public IActionResult createOrder()
        {
            return View();
        }
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> createOrder(PurchaseOrder order)
        {
            string? From = order.PurchaseFromLocation ;
            //DateTime Date =order.PurchaseDate;
            DateTime orderDate = order.PurchaseDate;
            decimal cost= order.ShipmentCost;
            int orderId= order.OrderId;

            /*string orderDate2 = Convert.ToString(Date);
            string[] orderDate1 = new string[2];
            orderDate1 = orderDate2.Split(" ");
            string orderDate = orderDate1[0];*/

            var insertRow = "InsertPurchaseOrder";
            var param = new { PurchaseFromLocation = From,  PurchaseDate = orderDate, ShipmentCost = cost, OrderId = orderId };

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                var PurchaseObject = await connection.ExecuteAsync(insertRow, param, commandType: CommandType.StoredProcedure);
            }
            Console.WriteLine("Insert operation Completed..");
            Console.WriteLine();
            
            
            ViewBag.PurchaseFromLocation = "Purchase From : " + From;
            ViewBag.PurchaseDate = "Purchase Date : " + orderDate;
            ViewBag.ShipmentCost = "Shipment Cost : " + cost;
            ViewBag.OrderId = "Order Id : " + orderId;

            return await Task.Run(() => View());
        }


        [HttpGet]
        [Route("Read")]
        [ResponseCache(Duration = 30)]
        public IActionResult readOrder()
        {
            return View();
        }
        [HttpPost]
        [Route("Read")]
        public IActionResult readOrder(PurchaseOrder orders)
        {
            return RedirectToAction("readOrderDetails", new { PurshaseId = Convert.ToInt32(orders.PurchaseId) });
        }
        [OutputCache(Duration = 30)]
        [Route("ReadId/{PurshaseId}")]
        public async Task<IActionResult> readOrderDetails(int PurshaseId)
        {
            PurchaseOrder Purchase;
            Console.WriteLine("Select operation Completed...");
            Console.WriteLine();
            var selectRowDetail = "select * from PurchaseOrder where PurchaseId = @Id";
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                Purchase = await connection.QueryFirstOrDefaultAsync<PurchaseOrder>(selectRowDetail, new { Id = PurshaseId });
            }
            return await Task.Run(() => View(Purchase));
        }
        

        [HttpGet]
        [Route("Update")]
        [Authorize]
        public IActionResult updateOrder()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> updateOrderAmount()
        {

            var updateTable = "UpdateTotalAmount";

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                var PurchaseList = await connection.ExecuteAsync(updateTable, commandType: CommandType.StoredProcedure);
            }
            Console.WriteLine("Update Purchase amount Completed..");

            ViewData["Message"] = "Purchase order - Total Amount updated";
            return await Task.Run(() => RedirectToAction("viewOrder"));
        }
        public async Task<IActionResult> updateOrderAddress(PurchaseOrder orders)
        {

            int PurshaseId = orders.PurchaseId;

            var updateRow = "UpdatePurchaseAdress";
            var param = new { PurchaseId = PurshaseId };
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                var PurchaseList = await connection.ExecuteAsync(updateRow, param, commandType: CommandType.StoredProcedure);
            }
            Console.WriteLine("Update operation Completed..");

            ViewData["Message"] = "Purchase Id : " + PurshaseId + "'s - Purchase TO Address updated";
            return await Task.Run(() => RedirectToAction("viewOrder"));
        }


        [HttpGet]
        [Route("Delete")]
        [Authorize]
        public IActionResult deleteOrder()
        {
            return  View();
        }
        [HttpPost]
        [Route("Delete")]
        public async Task<IActionResult> deleteOrder(PurchaseOrder orders)
        {

            int PurshaseId = orders.PurchaseId;

            var selectRowDetail = "delete from PurchaseOrder where PurchaseId = @Id";
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                var PurchaseList = await connection.ExecuteAsync(selectRowDetail, new { Id = PurshaseId });
            }
            Console.WriteLine("Delete operation Completed..");
            Console.WriteLine();

            ViewData["Message"] = "Purchase Id : " + PurshaseId+ " deleted";
            return await Task.Run(() => View());
        }


        [Route("Welcome")]
        public IActionResult Welcome(string name="logha")
        {
            ViewData["Message"] =name;
            return View();
        }


    }
}