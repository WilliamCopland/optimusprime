var level = require('level')
var db = level('./db')

var Jobs = require('level-jobs')

var queueOptions = {
  maxConcurrency: 1,
  maxRetries: 10,
  backoff: {
    randomisationFactor: 0,
    initialDelay: 10,
    maxDelay: 300
  }
}

var queue = Jobs(db, worker, queueOptions)

module.exports = queue

function worker(event, cb) {
  setTimeout(function() {
    //do the work that is being asked.
    console.log(event.event)
    cb()
  }, 100);
}
