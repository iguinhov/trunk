/*

  Multi-file load jQuery plugin
  
  Author: Frantisek Toman, www.frantatoman.cz
  License: Creative Commons Attribution 2.5 License, http://creativecommons.org/licenses/by/2.5/deed.en_GB
  

  Attributes: limit                 - max count of file inputs (0 or 1 = no limit)
              limitMessage          - show limit exceeded message  
              limitMessageText      - text of limit exceeded message
              limitMessagePostfix   - postfix message (e.g. files or images)
              unique                - unique selected files
        

*/

(function($){$.fn.extend({multifile:function(settings){var id=0;var count=0;var nameInput='';var selectedFiles=new Array(100000);var defaults={limit:1,limitMessage:false,limitMessageText:"You can select only ",limitMessagePostfix:"files",uniqueFile:false};var options=$.extend(defaults,settings);var addArrayItem=function(val){selectedFiles[id]=val};var removeArrayItem=function(val){for(var i=0;i<selectedFiles.length;i++){if(selectedFiles[i]==val){selectedFiles[i]="";break}}};var isInArray=function(val){for(var i=0;i<selectedFiles.length;i++){if(selectedFiles[i]==val){return true}}return false};var getNewInput=function(){var el=document.createElement("input");el.setAttribute("type","file");el.setAttribute("id","file_"+id++);el.setAttribute("name",nameInput);return el};var getNewLi=function(element){var removeEl=document.createElement("a");var newLi=document.createElement("li");var newText=document.createTextNode($(element).val());var liSpan=document.createElement("span");removeEl.setAttribute("href","#");liSpan.appendChild(newText);newLi.setAttribute("id",element.id+"li");$(removeEl).click(function(){removeItem(element,newLi);return false});newLi.appendChild(liSpan);newLi.appendChild(removeEl);return newLi};var removeItem=function(item,li){$(item).remove();$(li).remove();count--;if(count<defaults.limit&&defaults.limit>1){$("#file_"+(id-1)).attr("disabled","")}if(count===0){$("#multifile-list").fadeOut()}if(defaults.unique){removeArrayItem($(item).val())}};var addNext=function(child){if(isInArray($(child).val())&&defaults.unique){alert("This file is already selected. Please, select other file ...");return false}var newFile=getNewInput();$(child).before(newFile);$(newFile).change(function(){addNext(this)});count++;addArrayItem($(child).val());if(defaults.limit<=count&&defaults.limit>1){$(newFile).attr("disabled","disabled");if(defaults.limitMessage){alert(defaults.limitMessageText+" "+defaults.limit+" "+defaults.limitMessagePostfix+".")}}var newLi=getNewLi(child);$(child).fadeOut();$("#multifile-list ul").append(newLi);if(count>0){$("#multifile-list").fadeIn()}};return this.each(function(){$(this).change(function(){nameInput=this.name;var el=getNewInput();$(this).before(el);$(this).fadeOut();$(el).change(function(){addNext(this)});var listFiles=document.createElement("div");var ul=document.createElement("ul");var li=getNewLi(this);listFiles.setAttribute("id","multifile-list");ul.appendChild(li);listFiles.appendChild(ul);$(this).after(listFiles);id++;count++;addArrayItem($(this).val())})})}})})(jQuery);