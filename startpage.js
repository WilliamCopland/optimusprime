module.exports = class StartPage {
  constructor (document) {
    this.data = { title: 'Fate rarely calls upon us at a moment of our choosing.' }
  }

  load(document) {
    let mainDiv = document.getElementById("main")
    let compiledTemplate = Handlebars.templates['startpage']
    let result = compiledTemplate(this.data)
    mainDiv.innerHTML = result
  }

}
