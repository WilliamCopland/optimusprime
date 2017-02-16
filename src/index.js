var rpc = require('json-rpc2')

var server = rpc.Server.$create({
    'websocket': true,
    'headers': {
        'Access-Control-Allow-Origin': '*'
    }
})

function add(args, opt, callback) {
  callback(null, args[0] + args[1]);
}

server.expose('add', add);

server.listen(8000, 'localhost');
