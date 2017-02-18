var queue = require('./job_queue')

for(var i = 0 ; i < 10; i ++) {
  queue.push({id: i, event: 'door opened', when: Date.now()}, function(err) {
    if(err) throw err
    console.log('pushed')
  })
}
