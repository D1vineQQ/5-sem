(function (cjs, an) {

var p; // shortcut to reference prototypes
var lib={};var ss={};var img={};
lib.ssMetadata = [
		{name:"map_atlas_1", frames: [[0,0,1922,983]]}
];


(lib.AnMovieClip = function(){
	this.actionFrames = [];
	this.ignorePause = false;
	this.gotoAndPlay = function(positionOrLabel){
		cjs.MovieClip.prototype.gotoAndPlay.call(this,positionOrLabel);
	}
	this.play = function(){
		cjs.MovieClip.prototype.play.call(this);
	}
	this.gotoAndStop = function(positionOrLabel){
		cjs.MovieClip.prototype.gotoAndStop.call(this,positionOrLabel);
	}
	this.stop = function(){
		cjs.MovieClip.prototype.stop.call(this);
	}
}).prototype = p = new cjs.MovieClip();
// symbols:



(lib.top_view_on = function() {
	this.initialize(ss["map_atlas_1"]);
	this.gotoAndStop(0);
}).prototype = p = new cjs.Sprite();



(lib.volt_meter = function(mode,startPosition,loop,reversed) {
if (loop == null) { loop = true; }
if (reversed == null) { reversed = false; }
	var props = new Object();
	props.mode = mode;
	props.startPosition = startPosition;
	props.labels = {};
	props.loop = loop;
	props.reversed = reversed;
	cjs.MovieClip.apply(this,[props]);

	// timeline functions:
	this.frame_2 = function() {
		playSound("вольтметр");
	}

	// actions tween:
	this.timeline.addTween(cjs.Tween.get(this).wait(2).call(this.frame_2).wait(2));

	// Слой_1
	this.shape = new cjs.Shape();
	this.shape.graphics.f().s("#000000").ss(1,1,1).p("EAYEAn7MgxhgBQIi+iCMABuhJlICMiqMAztgAUIBQFoMgBaBHjg");
	this.shape.setTransform(182,255.475);

	this.shape_1 = new cjs.Shape();
	this.shape_1.graphics.f("#946B54").s().p("EgZdAmrIi+iCMABuhJlICMiqMAztgAUIBQFoMgBaBHjIi+Cqg");
	this.shape_1.setTransform(182,255.475);

	this.timeline.addTween(cjs.Tween.get({}).to({state:[]}).to({state:[{t:this.shape}]},1).to({state:[{t:this.shape}]},1).to({state:[{t:this.shape_1},{t:this.shape}]},1).wait(1));

	this._renderFirstFrame();

}).prototype = p = new cjs.MovieClip();
p.nominalBounds = new cjs.Rectangle(-1,-1,366,513);


(lib.switcher_1 = function(mode,startPosition,loop,reversed) {
if (loop == null) { loop = true; }
if (reversed == null) { reversed = false; }
	var props = new Object();
	props.mode = mode;
	props.startPosition = startPosition;
	props.labels = {};
	props.loop = loop;
	props.reversed = reversed;
	cjs.MovieClip.apply(this,[props]);

	// timeline functions:
	this.frame_2 = function() {
		playSound("switcher");
	}

	// actions tween:
	this.timeline.addTween(cjs.Tween.get(this).wait(2).call(this.frame_2).wait(2));

	// Слой_1
	this.shape = new cjs.Shape();
	this.shape.graphics.f().s("#000000").ss(1,1,1).p("Aixp5IlhCUIjEEqIAyGSIC/D0IGLCwIHOg4IDojjIB7kwIgMkzIjLjgIlFiXg");
	this.shape.setTransform(72.675,63.525);

	this.shape_1 = new cjs.Shape();
	this.shape_1.graphics.f("#00FFFF").s().p("AnlHLIi/j0IgymSIDEkqIFhiUIFsgBIFFCXIDLDgIAMEzIh7EwIjoDjInOA4g");
	this.shape_1.setTransform(72.675,63.525);

	this.timeline.addTween(cjs.Tween.get({}).to({state:[]}).to({state:[{t:this.shape}]},1).to({state:[{t:this.shape}]},1).to({state:[{t:this.shape_1},{t:this.shape}]},1).wait(1));

	this._renderFirstFrame();

}).prototype = p = new cjs.MovieClip();
p.nominalBounds = new cjs.Rectangle(-1,-1,147.4,129.1);


(lib.PowerSource = function(mode,startPosition,loop,reversed) {
if (loop == null) { loop = true; }
if (reversed == null) { reversed = false; }
	var props = new Object();
	props.mode = mode;
	props.startPosition = startPosition;
	props.labels = {};
	props.loop = loop;
	props.reversed = reversed;
	cjs.MovieClip.apply(this,[props]);

	// timeline functions:
	this.frame_2 = function() {
		playSound("Реостат");
	}

	// actions tween:
	this.timeline.addTween(cjs.Tween.get(this).wait(2).call(this.frame_2).wait(2));

	// Слой_1
	this.shape = new cjs.Shape();
	this.shape.graphics.f().s("#000000").ss(1,1,1).p("A0r0uIhkAdIg1A7IiHbnIA7B1ILMCqIBBHKIKXA1IBemPIQsA1IBGheIA1oLIGVgLIAenPImKgMIAGpAIskoug");
	this.shape.setTransform(161.175,132.7);

	this.shape_1 = new cjs.Shape();
	this.shape_1.graphics.f("#00FFFF").s().p("AsDT6IhBnKIrMiqIg7h1ICH7nIA1g6IBkgeIbPBGIMkIuIgGI/IGKANIgeHOImVAMIg1IKIhGBfIwsg2IheGQg");
	this.shape_1.setTransform(161.175,132.7);

	this.timeline.addTween(cjs.Tween.get({}).to({state:[]}).to({state:[{t:this.shape}]},1).to({state:[{t:this.shape}]},1).to({state:[{t:this.shape_1},{t:this.shape}]},1).wait(1));

	this._renderFirstFrame();

}).prototype = p = new cjs.MovieClip();
p.nominalBounds = new cjs.Rectangle(-1,-1,324.4,267.4);


(lib.photoresistor = function(mode,startPosition,loop,reversed) {
if (loop == null) { loop = true; }
if (reversed == null) { reversed = false; }
	var props = new Object();
	props.mode = mode;
	props.startPosition = startPosition;
	props.labels = {};
	props.loop = loop;
	props.reversed = reversed;
	cjs.MovieClip.apply(this,[props]);

	// timeline functions:
	this.frame_2 = function() {
		playSound("фоторезистор");
	}

	// actions tween:
	this.timeline.addTween(cjs.Tween.get(this).wait(2).call(this.frame_2).wait(2));

	// Слой_1
	this.shape = new cjs.Shape();
	this.shape.graphics.f().s("#000000").ss(1,1,1).p("AFFiBI4hgeIh4CpIhGDSIAoD6MAj7AA8IBaiCIGQkiIAophImkAeIhag8Ii0AyIhuCMg");
	this.shape.setTransform(143.475,53);

	this.shape_1 = new cjs.Shape();
	this.shape_1.graphics.f("#946B54").s().p("A1yHWIgoj6IBGjSIB4ipIYhAeIE2jSIBuiMIC0gyIBaA8IGkgeIgoJhImQEiIhaCCg");
	this.shape_1.setTransform(143.475,53);

	this.timeline.addTween(cjs.Tween.get({}).to({state:[]}).to({state:[{t:this.shape}]},1).to({state:[{t:this.shape}]},1).to({state:[{t:this.shape_1},{t:this.shape}]},1).wait(1));

	this._renderFirstFrame();

}).prototype = p = new cjs.MovieClip();
p.nominalBounds = new cjs.Rectangle(-1,-1,289,108);


(lib.distanse_meter = function(mode,startPosition,loop,reversed) {
if (loop == null) { loop = true; }
if (reversed == null) { reversed = false; }
	var props = new Object();
	props.mode = mode;
	props.startPosition = startPosition;
	props.labels = {};
	props.loop = loop;
	props.reversed = reversed;
	cjs.MovieClip.apply(this,[props]);

	// timeline functions:
	this.frame_2 = function() {
		playSound("линейкаизмерения");
	}

	// actions tween:
	this.timeline.addTween(cjs.Tween.get(this).wait(2).call(this.frame_2).wait(2));

	// Слой_1
	this.shape = new cjs.Shape();
	this.shape.graphics.f().s("#000000").ss(1,1,1).p("Eh7VgKsIhQQtMD5LAHMIgUxLIkYgKIgKhQMjvMgH0g");
	this.shape.setTransform(797.4,84.475);

	this.shape_1 = new cjs.Shape();
	this.shape_1.graphics.f("#946B54").s().p("Eh8lAGBIBQwtID5igMDvMAH0IAKBQIEYAKIAURLg");
	this.shape_1.setTransform(797.4,84.475);

	this.timeline.addTween(cjs.Tween.get({}).to({state:[]}).to({state:[{t:this.shape}]},1).to({state:[{t:this.shape}]},1).to({state:[{t:this.shape_1},{t:this.shape}]},1).wait(1));

	this._renderFirstFrame();

}).prototype = p = new cjs.MovieClip();
p.nominalBounds = new cjs.Rectangle(-1,-1,1596.8,171);


(lib.bulb = function(mode,startPosition,loop,reversed) {
if (loop == null) { loop = true; }
if (reversed == null) { reversed = false; }
	var props = new Object();
	props.mode = mode;
	props.startPosition = startPosition;
	props.labels = {};
	props.loop = loop;
	props.reversed = reversed;
	cjs.MovieClip.apply(this,[props]);

	// timeline functions:
	this.frame_2 = function() {
		playSound("источниксвета");
	}

	// actions tween:
	this.timeline.addTween(cjs.Tween.get(this).wait(2).call(this.frame_2).wait(2));

	// Слой_1
	this.shape = new cjs.Shape();
	this.shape.graphics.f().s("#000000").ss(1,1,1).p("AMymzIi8i2IkQhQIjzAqIheA0Ij8AJIj4BeIjBCaIh+CpIhCCDIggC7IAlCyIBLCRICNCIIDXBeIDqAEIDBghIC/hZICRhwICpgcICthUICbjBIAui1IATjKg");
	this.shape.setTransform(89.725,69.825);

	this.shape_1 = new cjs.Shape();
	this.shape_1.graphics.f("#00FFFF").s().p("AmsK2IjXheIiNiIIhLiRIgliyIAgi7IBCiDIB+ipIDBiaID4heID8gJIBeg0IDzgqIEQBQIC8C2IBPDTIgTDKIguC1IibDBIitBUIipAcIiRBwIi/BZIjBAhg");
	this.shape_1.setTransform(89.725,69.825);

	this.timeline.addTween(cjs.Tween.get({}).to({state:[]}).to({state:[{t:this.shape}]},1).to({state:[{t:this.shape}]},1).to({state:[{t:this.shape_1},{t:this.shape}]},1).wait(1));

	this._renderFirstFrame();

}).prototype = p = new cjs.MovieClip();
p.nominalBounds = new cjs.Rectangle(-1,-1,181.5,141.7);


(lib.amper_meter = function(mode,startPosition,loop,reversed) {
if (loop == null) { loop = true; }
if (reversed == null) { reversed = false; }
	var props = new Object();
	props.mode = mode;
	props.startPosition = startPosition;
	props.labels = {};
	props.loop = loop;
	props.reversed = reversed;
	cjs.MovieClip.apply(this,[props]);

	// timeline functions:
	this.frame_2 = function() {
		playSound("миллиамперметр");
	}

	// actions tween:
	this.timeline.addTween(cjs.Tween.get(this).wait(2).call(this.frame_2).wait(2));

	// Слой_1
	this.shape = new cjs.Shape();
	this.shape.graphics.f().s("#000000").ss(1,1,1).p("EggLAjoICgDIMA6RACgIBuhuMAB4gyJIhkkOIAo1GIiqi0Mgz3gCgIksD6g");
	this.shape.setTransform(206,263.975);

	this.shape_1 = new cjs.Shape();
	this.shape_1.graphics.f("#946B54").s().p("EgdrAmwIigjIMAEOhI9IEsj6MAz3ACgICqC0IgoVGIBkEOMgB4AyJIhuBug");
	this.shape_1.setTransform(206,263.975);

	this.timeline.addTween(cjs.Tween.get({}).to({state:[]}).to({state:[{t:this.shape}]},1).to({state:[{t:this.shape}]},1).to({state:[{t:this.shape_1},{t:this.shape}]},1).wait(1));

	this._renderFirstFrame();

}).prototype = p = new cjs.MovieClip();
p.nominalBounds = new cjs.Rectangle(-1,-1,414,530);


// stage content:
(lib.Безымянный1 = function(mode,startPosition,loop,reversed) {
if (loop == null) { loop = true; }
if (reversed == null) { reversed = false; }
	var props = new Object();
	props.mode = mode;
	props.startPosition = startPosition;
	props.labels = {};
	props.loop = loop;
	props.reversed = reversed;
	cjs.MovieClip.apply(this,[props]);

	// Слой_1
	this.instance = new lib.amper_meter();
	this.instance.setTransform(1590.9,326.95,1,1,0,0,0,206,263.9);
	new cjs.ButtonHelper(this.instance, 0, 1, 2, false, new lib.amper_meter(), 3);

	this.instance_1 = new lib.volt_meter();
	this.instance_1.setTransform(1068.9,317.55,1,1,0,0,0,182,255.5);
	new cjs.ButtonHelper(this.instance_1, 0, 1, 2, false, new lib.volt_meter(), 3);

	this.instance_2 = new lib.photoresistor();
	this.instance_2.setTransform(592.45,868.9,1,1,0,0,0,143.5,53);
	new cjs.ButtonHelper(this.instance_2, 0, 1, 2, false, new lib.photoresistor(), 3);

	this.instance_3 = new lib.PowerSource();
	this.instance_3.setTransform(499.35,261.25,1,1,0,0,0,161.2,132.7);
	new cjs.ButtonHelper(this.instance_3, 0, 1, 2, false, new lib.PowerSource(), 3);

	this.instance_4 = new lib.bulb();
	this.instance_4.setTransform(99.5,858,1,1,0,0,0,89.7,69.8);
	new cjs.ButtonHelper(this.instance_4, 0, 1, 2, false, new lib.bulb(), 3);

	this.instance_5 = new lib.switcher_1();
	this.instance_5.setTransform(153.15,422.2);
	new cjs.ButtonHelper(this.instance_5, 0, 1, 2, false, new lib.switcher_1(), 3);

	this.timeline.addTween(cjs.Tween.get({}).to({state:[{t:this.instance_5},{t:this.instance_4},{t:this.instance_3},{t:this.instance_2},{t:this.instance_1},{t:this.instance}]}).wait(1));

	// Слой_3
	this.instance_6 = new lib.distanse_meter();
	this.instance_6.setTransform(1055.45,862.45,1,1,0,0,0,797.4,84.5);
	new cjs.ButtonHelper(this.instance_6, 0, 1, 2, false, new lib.distanse_meter(), 3);

	this.timeline.addTween(cjs.Tween.get(this.instance_6).wait(1));

	// Слой_2
	this.instance_7 = new lib.top_view_on();

	this.timeline.addTween(cjs.Tween.get(this.instance_7).wait(1));

	this._renderFirstFrame();

}).prototype = p = new lib.AnMovieClip();
p.nominalBounds = new cjs.Rectangle(961,491.5,961,491.5);
// library properties:
lib.properties = {
	id: 'FCEF6A4A649C6F4E92DE89E0872A83F1',
	width: 1922,
	height: 983,
	fps: 24,
	color: "#FFFFFF",
	opacity: 1.00,
	manifest: [
		{src:"images/map_atlas_1.png", id:"map_atlas_1"},
		{src:"sounds/Реостат_.mp3", id:"Реостат"},
		{src:"sounds/вольтметр_.mp3", id:"вольтметр"},
		{src:"sounds/источниксвета_.mp3", id:"источниксвета"},
		{src:"sounds/линейкаизмерения_.mp3", id:"линейкаизмерения"},
		{src:"sounds/миллиамперметр_.mp3", id:"миллиамперметр"},
		{src:"sounds/фоторезистор_.mp3", id:"фоторезистор"},
		{src:"sounds/switcher.mp3", id:"switcher"}
	],
	preloads: []
};



// bootstrap callback support:

(lib.Stage = function(canvas) {
	createjs.Stage.call(this, canvas);
}).prototype = p = new createjs.Stage();

p.setAutoPlay = function(autoPlay) {
	this.tickEnabled = autoPlay;
}
p.play = function() { this.tickEnabled = true; this.getChildAt(0).gotoAndPlay(this.getTimelinePosition()) }
p.stop = function(ms) { if(ms) this.seek(ms); this.tickEnabled = false; }
p.seek = function(ms) { this.tickEnabled = true; this.getChildAt(0).gotoAndStop(lib.properties.fps * ms / 1000); }
p.getDuration = function() { return this.getChildAt(0).totalFrames / lib.properties.fps * 1000; }

p.getTimelinePosition = function() { return this.getChildAt(0).currentFrame / lib.properties.fps * 1000; }

an.bootcompsLoaded = an.bootcompsLoaded || [];
if(!an.bootstrapListeners) {
	an.bootstrapListeners=[];
}

an.bootstrapCallback=function(fnCallback) {
	an.bootstrapListeners.push(fnCallback);
	if(an.bootcompsLoaded.length > 0) {
		for(var i=0; i<an.bootcompsLoaded.length; ++i) {
			fnCallback(an.bootcompsLoaded[i]);
		}
	}
};

an.compositions = an.compositions || {};
an.compositions['FCEF6A4A649C6F4E92DE89E0872A83F1'] = {
	getStage: function() { return exportRoot.stage; },
	getLibrary: function() { return lib; },
	getSpriteSheet: function() { return ss; },
	getImages: function() { return img; }
};

an.compositionLoaded = function(id) {
	an.bootcompsLoaded.push(id);
	for(var j=0; j<an.bootstrapListeners.length; j++) {
		an.bootstrapListeners[j](id);
	}
}

an.getComposition = function(id) {
	return an.compositions[id];
}


an.makeResponsive = function(isResp, respDim, isScale, scaleType, domContainers) {		
	var lastW, lastH, lastS=1;		
	window.addEventListener('resize', resizeCanvas);		
	resizeCanvas();		
	function resizeCanvas() {			
		var w = lib.properties.width, h = lib.properties.height;			
		var iw = window.innerWidth, ih=window.innerHeight;			
		var pRatio = window.devicePixelRatio || 1, xRatio=iw/w, yRatio=ih/h, sRatio=1;			
		if(isResp) {                
			if((respDim=='width'&&lastW==iw) || (respDim=='height'&&lastH==ih)) {                    
				sRatio = lastS;                
			}				
			else if(!isScale) {					
				if(iw<w || ih<h)						
					sRatio = Math.min(xRatio, yRatio);				
			}				
			else if(scaleType==1) {					
				sRatio = Math.min(xRatio, yRatio);				
			}				
			else if(scaleType==2) {					
				sRatio = Math.max(xRatio, yRatio);				
			}			
		}
		domContainers[0].width = w * pRatio * sRatio;			
		domContainers[0].height = h * pRatio * sRatio;
		domContainers.forEach(function(container) {				
			container.style.width = w * sRatio + 'px';				
			container.style.height = h * sRatio + 'px';			
		});
		stage.scaleX = pRatio*sRatio;			
		stage.scaleY = pRatio*sRatio;
		lastW = iw; lastH = ih; lastS = sRatio;            
		stage.tickOnUpdate = false;            
		stage.update();            
		stage.tickOnUpdate = true;		
	}
}
an.handleSoundStreamOnTick = function(event) {
	if(!event.paused){
		var stageChild = stage.getChildAt(0);
		if(!stageChild.paused || stageChild.ignorePause){
			stageChild.syncStreamSounds();
		}
	}
}
an.handleFilterCache = function(event) {
	if(!event.paused){
		var target = event.target;
		if(target){
			if(target.filterCacheList){
				for(var index = 0; index < target.filterCacheList.length ; index++){
					var cacheInst = target.filterCacheList[index];
					if((cacheInst.startFrame <= target.currentFrame) && (target.currentFrame <= cacheInst.endFrame)){
						cacheInst.instance.cache(cacheInst.x, cacheInst.y, cacheInst.w, cacheInst.h);
					}
				}
			}
		}
	}
}


})(createjs = createjs||{}, AdobeAn = AdobeAn||{});
var createjs, AdobeAn;