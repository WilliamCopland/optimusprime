"use strict"

var {ipcRenderer, remote} = require('electron')
var main = remote.require("./main.js")
var Handlebars = require('handlebars')
var templates = require('./templates/templates.js')
var StartPage = require('./startpage')

document.addEventListener("DOMContentLoaded", function() {
    let version = process.version
    var startPage = new StartPage()
    startPage.load(document)
})

ipcRenderer.on('testing', function () {
  console.log('hello world, this is from index.js testing')
})
