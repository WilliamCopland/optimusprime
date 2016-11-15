var express = require('express');
var app = express();
var bodyParser = require('body-parser')

var edge = require('edge');
var hpvResultHandler = edge.func({ assemblyFile: 'YellowstonePathology.OptimusPrime.dll', typeName: 'YellowstonePathology.OptimusPrime.HPVResultHandler', methodName: 'Invoke' });
var ngctResultHandler = edge.func({ assemblyFile: 'YellowstonePathology.OptimusPrime.dll', typeName: 'YellowstonePathology.OptimusPrime.NGCTResultHandler', methodName: 'Invoke' });
var hpv1618ResultHandler = edge.func({ assemblyFile: 'YellowstonePathology.OptimusPrime.dll', typeName: 'YellowstonePathology.OptimusPrime.HPV1618ResultHandler', methodName: 'Invoke' });
var trichResultHandler = edge.func({ assemblyFile: 'YellowstonePathology.OptimusPrime.dll', typeName: 'YellowstonePathology.OptimusPrime.TrichomonasResultHandler', methodName: 'Invoke' });

app.use(bodyParser.urlencoded({ extended: false }))
app.use(bodyParser.json())

app.get('/', function (req, res) {
  res.send('I am Optimus Prime, and I send this message to any surviving Autobots taking refuge among the stars. We are here, we are waiting.');
});

app.post('/panther', function(req, res) {
  console.log("Optimus Prime received result from Panther: " + req.body.TestName + " - " + req.body.AliquotOrderId)
  res.send("Result Received") 

  if(req.body.TestName == 'HPV')
  {
	var hpvData = { aliquotOrderId: req.body.AliquotOrderId,
		reportNo: req.body.ReportNo,
		lastName: req.body.LastName,
		firstName: req.body.FirstName,
		testName: req.body.TestName,
		overallInterpretation: req.body.OverallInterpretation
	};
  
	hpvResultHandler(hpvData, function (error, result) {
		if (error) { console.log(error); return; }
		if (result) {
			console.log(result)
		}
		else {
			res.end("No results");
		}
	});  
  } 
  else if(req.body.TestName == 'CT/GC')
  {
	  var ngctData = { aliquotOrderId: req.body.AliquotOrderId,
		reportNo: req.body.ReportNo,
		lastName: req.body.LastName,
		firstName: req.body.FirstName,
		testName: req.body.TestName,
		ngResult: req.body.GCResult,
		ctResult: req.body.CTResult
	};
	
	ngctResultHandler(ngctData, function (error, result) {
		if (error) { console.log(error); return; }
		if (result) {
			console.log(result);			
		}
		else {
			res.end("No results");
		}
	});  
  }
  else if(req.body.TestName == 'GT HPV') {
	var hpv1618Data = { aliquotOrderId: req.body.AliquotOrderId,
		reportNo: req.body.ReportNo,
		lastName: req.body.LastName,
		firstName: req.body.FirstName,
		testName: req.body.TestName,
		hpv16Result: req.body['HPV 16 Result'],
		hpv1845Result: req.body['HPV 18/45 Result']
	};
	
	hpv1618ResultHandler(hpv1618Data, function (error, result) {
		if (error) { console.log(error); return; }
		if (result) {
			console.log(result);			
		}
		else {
			res.end("No results");
		}
	});  
  }	 
  else if(req.body.TestName == 'TRICH') {
	var trichData = { aliquotOrderId: req.body.AliquotOrderId,
		reportNo: req.body.ReportNo,
		lastName: req.body.LastName,
		firstName: req.body.FirstName,
		testName: req.body.TestName,
		result: req.body['TRICH Result']		
	};
	
	trichResultHandler(trichData, function (error, result) {
		if (error) { console.log(error); return; }
		if (result) {
			console.log(result);			
		}
		else {
			res.end("No results");
		}
	});  
  }	 
  else {
	console.log("Optimus Prime does not handle results of type: " + req.body.AliquotOrderId + " - " + req.body.TestName);
  }

});

var server = app.listen(3000, function () {
  var host = server.address().address;
  var port = server.address().port;

  console.log('Optimus Prime listening at http://%s:%s', host, port);
});
