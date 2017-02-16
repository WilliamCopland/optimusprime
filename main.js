"use strict"

const fs = require('fs')
const electron = require('electron')
const app = electron.app
const BrowserWindow = electron.BrowserWindow
const ipc = electron.ipcMain;
const dialog = electron.dialog;

let mainWindow = null;

electron.app.on('browser-window-created',function(e,window) {
      window.setMenu(null);
});

app.on( "window-all-closed", function() {
    if ( process.platform !== "darwin" ) {
        app.quit()
    }
})

app.on( "ready", function() {
    mainWindow = new BrowserWindow( { width: 1500, height: 1000, center: true, title: 'Optimus Prime' } )
    mainWindow.loadURL( "file://" + __dirname + "/index.html" )
    mainWindow.webContents.openDevTools();
    mainWindow.on( "closed", function() {
        mainWindow = null
    })
})

ipc.on('hyperlink', (event, arg) => {
    console.log(arg)
    event.sender.send('testing', 'hello from main.js')
    //event.sender.send('async-reply', 2)
})
