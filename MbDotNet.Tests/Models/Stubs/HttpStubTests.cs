﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using MbDotNet.Enums;
using MbDotNet.Models.Predicates;
using MbDotNet.Models.Predicates.Fields;
using MbDotNet.Models.Responses;
using MbDotNet.Models.Responses.Fields;
using MbDotNet.Models.Stubs;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MbDotNet.Tests.Models.Stubs
{
    [TestClass]
    public class HttpStubTests
    {
        [TestMethod]
        public void HttpStub_Constructor_InitializesResponsesCollection()
        {
            var stub = new HttpStub();
            Assert.IsNotNull(stub.Responses);
        }

        [TestMethod]
        public void HttpStub_Constructor_InitializesPredicatesCollection()
        {
            var stub = new HttpStub();
            Assert.IsNotNull(stub.Predicates);
        }

        [TestMethod]
        public void HttpStub_ReturnsStatus_AddsResponse_StatusCodeSet()
        {
            const HttpStatusCode expectedStatusCode = HttpStatusCode.OK;

            var stub = new HttpStub();
            stub.ReturnsStatus(expectedStatusCode);

            var response = stub.Responses.First() as IsResponse<HttpResponseFields>;
            Assert.IsNotNull(response);
            Assert.AreEqual(expectedStatusCode, response.Fields.StatusCode);
        }

        [TestMethod]
        public void HttpStub_Returns_AddsResponse_StatusCodeSet()
        {
            const HttpStatusCode expectedStatusCode = HttpStatusCode.OK;
            var headers = new Dictionary<string, string> { {"Content-Type", "application/json"} };

            var stub = new HttpStub();
            stub.Returns(expectedStatusCode, headers, "test");

            var response = stub.Responses.First() as IsResponse<HttpResponseFields>;
            Assert.IsNotNull(response);
            Assert.AreEqual(expectedStatusCode, response.Fields.StatusCode);
        }

        [TestMethod]
        public void HttpStub_Returns_AddsResponse_ResponseObjectSet()
        {
            const string expectedResponseObject = "Test Response";
            var headers = new Dictionary<string, string> { { "Content-Type", "application/json" } };

            var stub = new HttpStub();
            stub.Returns(HttpStatusCode.OK, headers, expectedResponseObject);

            var response = stub.Responses.First() as IsResponse<HttpResponseFields>;
            Assert.IsNotNull(response);
            Assert.AreEqual(expectedResponseObject, response.Fields.ResponseObject);
        }

        [TestMethod]
        public void HttpStub_Returns_AddsResponse_ContentTypeHeaderSet()
        {
            var headers = new Dictionary<string, string> { { "Content-Type", "application/json" } };

            var stub = new HttpStub();
            stub.Returns(HttpStatusCode.OK, headers, "test");

            var response = stub.Responses.First() as IsResponse<HttpResponseFields>;
            Assert.IsNotNull(response);
            Assert.AreEqual(headers, response.Fields.Headers);
        }

        [TestMethod]
        public void HttpStub_Returns_AddsResponse()
        {
            var expectedResponse = new IsResponse<HttpResponseFields>(new HttpResponseFields());

            var stub = new HttpStub();
            stub.Returns(expectedResponse);

            var response = stub.Responses.First() as IsResponse<HttpResponseFields>;
            Assert.AreEqual(expectedResponse, response);
        }

        [TestMethod]
        public void HttpStub_ReturnsBody_AddsResponse_StatusCodeSet()
        {
            const HttpStatusCode expectedStatusCode = HttpStatusCode.OK;

            var stub = new HttpStub();
            stub.ReturnsBody(expectedStatusCode, "test");

            var response = stub.Responses.First() as IsResponse<HttpResponseFields>;
            Assert.IsNotNull(response);
            Assert.AreEqual(expectedStatusCode, response.Fields.StatusCode);
        }

        [TestMethod]
        public void HttpStub_ReturnsBody_AddsResponse_ResponseObjectSet()
        {
            const string expectedBody = "test";

            var stub = new HttpStub();
            stub.ReturnsBody(HttpStatusCode.OK, expectedBody);

            var response = stub.Responses.First() as IsResponse<HttpResponseFields>;
            Assert.IsNotNull(response);
            Assert.AreEqual(expectedBody, response.Fields.ResponseObject.ToString());
        }

        [TestMethod]
        public void HttpStub_ReturnsXml_AddsResponse_StatusCodeSet()
        {
            const HttpStatusCode expectedStatusCode = HttpStatusCode.OK;

            var stub = new HttpStub();
            stub.ReturnsXml(expectedStatusCode, "test");

            var response = stub.Responses.First() as IsResponse<HttpResponseFields>;
            Assert.IsNotNull(response);
            Assert.AreEqual(expectedStatusCode, response.Fields.StatusCode);
        }

        [TestMethod]
        public void HttpStub_ReturnsXml_AddsResponse_ResponseObjectSerializedAndSet()
        {
            const string expectedResponseObject = "<string>Test Response</string>";

            var stub = new HttpStub();
            stub.ReturnsXml(HttpStatusCode.OK, "Test Response");

            var response = stub.Responses.First() as IsResponse<HttpResponseFields>;
            Assert.IsNotNull(response);
            Assert.IsTrue(response.Fields.ResponseObject.ToString().Contains(expectedResponseObject));
        }

        [TestMethod]
        public void HttpStub_ReturnsXml_AddsResponse_DefaultsToUtf8Encoding()
        {
            const string expectedEncoding = "utf-8";

            var stub = new HttpStub();
            stub.ReturnsXml(HttpStatusCode.OK, "Test Response");

            var response = stub.Responses.First() as IsResponse<HttpResponseFields>;
            Assert.IsNotNull(response);
            Assert.IsTrue(response.Fields.ResponseObject.ToString().Contains(expectedEncoding));
        }

        [TestMethod]
        public void HttpStub_ReturnsXmlWithEncoding_AddsResponse_WithSpecifiedEncoding()
        {
            const string expectedEncoding = "utf-16";

            var stub = new HttpStub();
            stub.ReturnsXml(HttpStatusCode.OK, "Test Response", Encoding.Unicode);

            var response = stub.Responses.First() as IsResponse<HttpResponseFields>;
            Assert.IsNotNull(response);
            Assert.IsTrue(response.Fields.ResponseObject.ToString().Contains(expectedEncoding));
        }

        [TestMethod]
        public void HttpStub_ReturnsXml_AddsResponse_ContentTypeHeaderSet()
        {
            var headers = new Dictionary<string, string> { { "Content-Type", "application/xml" } };

            var stub = new HttpStub();
            stub.ReturnsXml(HttpStatusCode.OK, "test");

            var response = stub.Responses.First() as IsResponse<HttpResponseFields>;
            Assert.IsNotNull(response);
            Assert.AreEqual(headers["Content-Type"], response.Fields.Headers["Content-Type"]);
        }

        [TestMethod]
        public void HttpStub_OnPathEquals_AddsPredicate_PathSet()
        {
            const string expectedPath = "/test";

            var stub = new HttpStub();
            stub.OnPathEquals(expectedPath);

            var predicate = stub.Predicates.First() as EqualsPredicate<HttpPredicateFields>;
            Assert.IsNotNull(predicate);
            Assert.AreEqual(expectedPath, predicate.Fields.Path);
        }

        [TestMethod]
        public void HttpStub_OnMethodEquals_AddsPredicate_MethodSet()
        {
            const Method expectedMethod = Method.Post;

            var stub = new HttpStub();
            stub.OnMethodEquals(expectedMethod);

            var predicate = stub.Predicates.First() as EqualsPredicate<HttpPredicateFields>;
            Assert.IsNotNull(predicate);
            Assert.AreEqual(expectedMethod, predicate.Fields.Method);
        }

        [TestMethod]
        public void HttpStub_OnPathAndMethodEqual_AddsPredicate_PathSet()
        {
            const string expectedPath = "/test";

            var stub = new HttpStub();
            stub.OnPathAndMethodEqual(expectedPath, Method.Get);

            var predicate = stub.Predicates.First() as EqualsPredicate<HttpPredicateFields>;
            Assert.IsNotNull(predicate);
            Assert.AreEqual(expectedPath, predicate.Fields.Path);
        }

        [TestMethod]
        public void HttpStub_OnPathAndMethodEqual_AddsPredicate_MethodSet()
        {
            const Method expectedMethod = Method.Post;

            var stub = new HttpStub();
            stub.OnPathAndMethodEqual("/test", expectedMethod);

            var predicate = stub.Predicates.First() as EqualsPredicate<HttpPredicateFields>;
            Assert.IsNotNull(predicate);
            Assert.AreEqual(expectedMethod, predicate.Fields.Method);
        }

        [TestMethod]
        public void HttpStub_On_AddsPredicate()
        {
            var expectedPredicate = new EqualsPredicate<HttpPredicateFields>(new HttpPredicateFields());

            var stub = new HttpStub();
            stub.On(expectedPredicate);

            var predicate = stub.Predicates.First() as EqualsPredicate<HttpPredicateFields>;
            Assert.AreEqual(expectedPredicate, predicate);
        }

        [TestMethod]
        public void HttpStub_On_ReturnsProxyStub()
        {            
            var predicateInvokingProxyStub = new ContainsPredicate<HttpPredicateFields>(new HttpPredicateFields
            {
                Path = "/aTestPath"
            });

            var proxyGeneratorPredicate = new MatchesPredicate<PredicateGeneratorFields>(new PredicateGeneratorFields
            {
                Path = true,
                Method = true,
                QueryParameters = true
            });

            var proxyToUrl = new Uri("http://someTestDestination.com");
            var proxyModeToUse = ProxyMode.ProxyTransparent;

            var stub = new HttpStub();
            stub.On(predicateInvokingProxyStub)
                .Returns(proxyToUrl, proxyModeToUse, new List<PredicateBase>() { proxyGeneratorPredicate });

            var proxyRepsonse = stub.Responses.First() as ProxyResponse<ProxyResponseFields>;

            Assert.AreEqual(proxyToUrl, proxyRepsonse.Fields.To);
            Assert.AreEqual(proxyModeToUse, proxyRepsonse.Fields.Mode);
            Assert.AreEqual(proxyGeneratorPredicate, proxyRepsonse.Fields.PredicateGenerators.First());
        }
    }
}
