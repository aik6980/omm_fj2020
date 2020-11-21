//Maya ASCII 2018ff09 scene
//Name: box_character_clip_stop_01.ma
//Last modified: Sat, Nov 21, 2020 02:25:14 PM
//Codeset: 1252
file -rdi 1 -ns "box_character_rig_01" -rfn "box_character_rig_01RN" -op "v=0;"
		 -typ "mayaAscii" "C:/Users/morten.zimmermann/Documents/maya/projects/oatMilkMojito_project//scenes/box_character_rig_01.ma";
file -r -ns "box_character_rig_01" -dr 1 -rfn "box_character_rig_01RN" -op "v=0;"
		 -typ "mayaAscii" "C:/Users/morten.zimmermann/Documents/maya/projects/oatMilkMojito_project//scenes/box_character_rig_01.ma";
requires maya "2018ff09";
requires -nodeType "polyDisc" "modelingToolkit" "0.0.0.0";
requires "stereoCamera" "10.0";
requires "mtoa" "3.1.2.1";
currentUnit -l centimeter -a degree -t ntsc;
fileInfo "application" "maya";
fileInfo "product" "Maya 2018";
fileInfo "version" "2018";
fileInfo "cutIdentifier" "201903222215-65bada0e52";
fileInfo "osv" "Microsoft Windows 8 Enterprise Edition, 64-bit  (Build 9200)\n";
createNode transform -s -n "persp";
	rename -uid "85C606D0-4B6C-A531-FFAA-A39BCC309FFB";
	setAttr ".v" no;
	setAttr ".t" -type "double3" 933.03842877828777 456.00914555117197 1010.3183840713519 ;
	setAttr ".r" -type "double3" -9.3383527294243258 -1036.2000000002893 -5.5083297707951936e-16 ;
createNode camera -s -n "perspShape" -p "persp";
	rename -uid "C6961C58-4B66-B63F-FE27-BEB90E17FF14";
	setAttr -k off ".v" no;
	setAttr ".fl" 34.999999999999993;
	setAttr ".coi" 563.86634049670295;
	setAttr ".imn" -type "string" "persp";
	setAttr ".den" -type "string" "persp_depth";
	setAttr ".man" -type "string" "persp_mask";
	setAttr ".tp" -type "double3" 0 -0.61104976038348013 6.6478454822788136 ;
	setAttr ".hc" -type "string" "viewSet -p %camera";
	setAttr ".ai_translator" -type "string" "perspective";
createNode transform -s -n "top";
	rename -uid "762555CF-4479-CEEB-73F5-E9B3C2A1C278";
	setAttr ".v" no;
	setAttr ".t" -type "double3" 0 1000.1 0 ;
	setAttr ".r" -type "double3" -89.999999999999986 0 0 ;
createNode camera -s -n "topShape" -p "top";
	rename -uid "4ABE5BCC-4303-03AB-0B47-DEA1DF4FDA7F";
	setAttr -k off ".v" no;
	setAttr ".rnd" no;
	setAttr ".coi" 1000.1;
	setAttr ".ow" 30;
	setAttr ".imn" -type "string" "top";
	setAttr ".den" -type "string" "top_depth";
	setAttr ".man" -type "string" "top_mask";
	setAttr ".tp" -type "double3" 0 -0.61104976038348013 6.6478454822788136 ;
	setAttr ".hc" -type "string" "viewSet -t %camera";
	setAttr ".o" yes;
	setAttr ".ai_translator" -type "string" "orthographic";
createNode transform -s -n "front";
	rename -uid "DFE08455-429A-58F3-AEE2-2D820A18D5A2";
	setAttr ".v" no;
	setAttr ".t" -type "double3" 0 0 1000.1 ;
createNode camera -s -n "frontShape" -p "front";
	rename -uid "90A0547A-4A1E-E9EC-6C78-089C3D95473B";
	setAttr -k off ".v" no;
	setAttr ".rnd" no;
	setAttr ".coi" 1000.1;
	setAttr ".ow" 30;
	setAttr ".imn" -type "string" "front";
	setAttr ".den" -type "string" "front_depth";
	setAttr ".man" -type "string" "front_mask";
	setAttr ".tp" -type "double3" 0 -0.61104976038348013 6.6478454822788136 ;
	setAttr ".hc" -type "string" "viewSet -f %camera";
	setAttr ".o" yes;
	setAttr ".ai_translator" -type "string" "orthographic";
createNode transform -s -n "side";
	rename -uid "632C8A64-4A7B-5B6A-2D1B-07922A9233E1";
	setAttr ".v" no;
	setAttr ".t" -type "double3" 1000.1 0 0 ;
	setAttr ".r" -type "double3" 0 89.999999999999986 0 ;
createNode camera -s -n "sideShape" -p "side";
	rename -uid "84CDAB8E-4455-7EB8-9013-AFAD8E6C53A1";
	setAttr -k off ".v" no;
	setAttr ".rnd" no;
	setAttr ".coi" 1000.1;
	setAttr ".ow" 30;
	setAttr ".imn" -type "string" "side";
	setAttr ".den" -type "string" "side_depth";
	setAttr ".man" -type "string" "side_mask";
	setAttr ".tp" -type "double3" 0 -0.61104976038348013 6.6478454822788136 ;
	setAttr ".hc" -type "string" "viewSet -s %camera";
	setAttr ".o" yes;
	setAttr ".ai_translator" -type "string" "orthographic";
createNode transform -n "pDisc1";
	rename -uid "DDFEACC1-47EC-0721-F1F4-749089FEE860";
	setAttr ".s" -type "double3" 860.97393391227138 860.97393391227138 860.97393391227138 ;
createNode mesh -n "pDiscShape1" -p "pDisc1";
	rename -uid "04BA889E-4522-EEEF-0BFA-DF9D8616BD76";
	setAttr -k off ".v";
	setAttr ".vir" yes;
	setAttr ".vif" yes;
	setAttr ".uvst[0].uvsn" -type "string" "map1";
	setAttr ".cuvs" -type "string" "map1";
	setAttr ".dcc" -type "string" "Ambient+Diffuse";
	setAttr ".covm[0]"  0 1 1;
	setAttr ".cdvm[0]"  0 1 1;
	setAttr ".ai_translator" -type "string" "polymesh";
createNode transform -n "directionalLight1";
	rename -uid "EEA9F7F2-4577-B2CD-3B60-18BA80AA5113";
	setAttr ".t" -type "double3" -1074.6907020836466 2283.8864564598953 490.4065187341198 ;
	setAttr ".r" -type "double3" -118.57323284682448 9.336072844479089 -58.449229427382207 ;
	setAttr ".s" -type "double3" 179.50630122109271 179.50630122109271 179.50630122109271 ;
createNode directionalLight -n "directionalLightShape1" -p "directionalLight1";
	rename -uid "0C6286B2-4C22-E6A2-A432-F89A2A045436";
	setAttr -k off ".v";
	setAttr ".cl" -type "float3" 1 0.93760002 0.72539997 ;
	setAttr ".urs" no;
createNode transform -n "directionalLight2";
	rename -uid "E65DE184-481D-0618-5941-AE97DEE2CD2B";
	setAttr ".t" -type "double3" -1033.3983552844466 2179.2640273604429 770.93697666682408 ;
	setAttr ".r" -type "double3" 58.571899499908568 51.095903175903302 -78.524768681160879 ;
	setAttr ".s" -type "double3" 179.50630122109271 179.50630122109271 179.50630122109271 ;
createNode directionalLight -n "directionalLightShape2" -p "directionalLight2";
	rename -uid "756419B5-4321-D92E-4EF9-D2880873AF87";
	setAttr -k off ".v";
	setAttr ".cl" -type "float3" 0.80239999 0.72530001 1 ;
	setAttr ".urs" no;
createNode transform -n "directionalLight3";
	rename -uid "3E4801FF-4B50-DC69-6E3F-6BB3C9ACD234";
	setAttr ".t" -type "double3" -1074.6907020836466 2283.8864564598953 490.4065187341198 ;
	setAttr ".r" -type "double3" -94.727829915482147 -44.758110907204561 -13.673649797191798 ;
	setAttr ".s" -type "double3" 179.50630122109271 179.50630122109271 179.50630122109271 ;
createNode directionalLight -n "directionalLightShape3q" -p "directionalLight3";
	rename -uid "BE61D23B-41D0-DDA8-AC91-F5BB78E2654D";
	setAttr -k off ".v";
	setAttr ".cl" -type "float3" 1 0.93760002 0.72539997 ;
createNode lightLinker -s -n "lightLinker1";
	rename -uid "1D7C78F2-4AEC-5C98-284E-058784295FB2";
	setAttr -s 10 ".lnk";
	setAttr -s 10 ".slnk";
createNode shapeEditorManager -n "shapeEditorManager";
	rename -uid "B726FC72-43AA-C5CD-DC86-A79EE2892CDB";
	setAttr ".bsdt[0].bscd" -type "Int32Array" 1 0 ;
createNode poseInterpolatorManager -n "poseInterpolatorManager";
	rename -uid "F8B7D304-42FA-4378-0080-13B1A409A031";
createNode displayLayerManager -n "layerManager";
	rename -uid "CB2274F8-4817-5A8D-F407-AAAC23F0AEB4";
createNode displayLayer -n "defaultLayer";
	rename -uid "C9D9B8F3-45DB-A4E2-63D0-E9A63D95370F";
createNode renderLayerManager -n "renderLayerManager";
	rename -uid "5657CAD8-4A79-B448-9B0A-A994A2BF8326";
createNode renderLayer -n "defaultRenderLayer";
	rename -uid "166FEFDA-4967-9D62-8C04-11A9C4ADA063";
	setAttr ".g" yes;
createNode reference -n "box_character_rig_01RN";
	rename -uid "A0407AAD-49BA-BF32-5CDB-48A19A53D633";
	setAttr -s 42 ".phl";
	setAttr ".phl[1]" 0;
	setAttr ".phl[2]" 0;
	setAttr ".phl[3]" 0;
	setAttr ".phl[4]" 0;
	setAttr ".phl[5]" 0;
	setAttr ".phl[6]" 0;
	setAttr ".phl[7]" 0;
	setAttr ".phl[8]" 0;
	setAttr ".phl[9]" 0;
	setAttr ".phl[10]" 0;
	setAttr ".phl[11]" 0;
	setAttr ".phl[12]" 0;
	setAttr ".phl[13]" 0;
	setAttr ".phl[14]" 0;
	setAttr ".phl[15]" 0;
	setAttr ".phl[16]" 0;
	setAttr ".phl[17]" 0;
	setAttr ".phl[18]" 0;
	setAttr ".phl[19]" 0;
	setAttr ".phl[20]" 0;
	setAttr ".phl[21]" 0;
	setAttr ".phl[22]" 0;
	setAttr ".phl[23]" 0;
	setAttr ".phl[24]" 0;
	setAttr ".phl[25]" 0;
	setAttr ".phl[26]" 0;
	setAttr ".phl[27]" 0;
	setAttr ".phl[28]" 0;
	setAttr ".phl[29]" 0;
	setAttr ".phl[30]" 0;
	setAttr ".phl[31]" 0;
	setAttr ".phl[32]" 0;
	setAttr ".phl[33]" 0;
	setAttr ".phl[34]" 0;
	setAttr ".phl[35]" 0;
	setAttr ".phl[36]" 0;
	setAttr ".phl[37]" 0;
	setAttr ".phl[38]" 0;
	setAttr ".phl[39]" 0;
	setAttr ".phl[40]" 0;
	setAttr ".phl[41]" 0;
	setAttr ".phl[42]" 0;
	setAttr ".ed" -type "dataReferenceEdits" 
		"box_character_rig_01RN"
		"box_character_rig_01RN" 0
		"box_character_rig_01RN" 103
		2 "|box_character_rig_01:chara_cube_main_ctrl" "translate" " -type \"double3\" 0 0 0"
		
		2 "|box_character_rig_01:chara_cube_main_ctrl" "translateX" " -av"
		2 "|box_character_rig_01:chara_cube_main_ctrl" "translateZ" " -av"
		2 "|box_character_rig_01:chara_cube_main_ctrl|box_character_rig_01:chara_cube_footR_ctrl" 
		"visibility" " -av 1"
		2 "|box_character_rig_01:chara_cube_main_ctrl|box_character_rig_01:chara_cube_footR_ctrl" 
		"translate" " -type \"double3\" 0 0 0"
		2 "|box_character_rig_01:chara_cube_main_ctrl|box_character_rig_01:chara_cube_footR_ctrl" 
		"translateX" " -av"
		2 "|box_character_rig_01:chara_cube_main_ctrl|box_character_rig_01:chara_cube_footR_ctrl" 
		"translateY" " -av"
		2 "|box_character_rig_01:chara_cube_main_ctrl|box_character_rig_01:chara_cube_footR_ctrl" 
		"translateZ" " -av"
		2 "|box_character_rig_01:chara_cube_main_ctrl|box_character_rig_01:chara_cube_footR_ctrl" 
		"rotate" " -type \"double3\" 0 0 0"
		2 "|box_character_rig_01:chara_cube_main_ctrl|box_character_rig_01:chara_cube_footR_ctrl" 
		"rotateX" " -av"
		2 "|box_character_rig_01:chara_cube_main_ctrl|box_character_rig_01:chara_cube_footR_ctrl" 
		"rotateY" " -av"
		2 "|box_character_rig_01:chara_cube_main_ctrl|box_character_rig_01:chara_cube_footR_ctrl" 
		"rotateZ" " -av"
		2 "|box_character_rig_01:chara_cube_main_ctrl|box_character_rig_01:chara_cube_footR_ctrl|box_character_rig_01:chara_cube_footR_geo|box_character_rig_01:chara_cube_footR_geoShape" 
		"dispResolution" " 1"
		2 "|box_character_rig_01:chara_cube_main_ctrl|box_character_rig_01:chara_cube_footR_ctrl|box_character_rig_01:chara_cube_footR_geo|box_character_rig_01:chara_cube_footR_geoShape" 
		"displaySmoothMesh" " 0"
		2 "|box_character_rig_01:chara_cube_main_ctrl|box_character_rig_01:chara_cube_footL_ctrl" 
		"visibility" " -av 1"
		2 "|box_character_rig_01:chara_cube_main_ctrl|box_character_rig_01:chara_cube_footL_ctrl" 
		"translate" " -type \"double3\" 0 0 0"
		2 "|box_character_rig_01:chara_cube_main_ctrl|box_character_rig_01:chara_cube_footL_ctrl" 
		"translateX" " -av"
		2 "|box_character_rig_01:chara_cube_main_ctrl|box_character_rig_01:chara_cube_footL_ctrl" 
		"translateY" " -av"
		2 "|box_character_rig_01:chara_cube_main_ctrl|box_character_rig_01:chara_cube_footL_ctrl" 
		"translateZ" " -av"
		2 "|box_character_rig_01:chara_cube_main_ctrl|box_character_rig_01:chara_cube_footL_ctrl" 
		"rotate" " -type \"double3\" 0 0 0"
		2 "|box_character_rig_01:chara_cube_main_ctrl|box_character_rig_01:chara_cube_footL_ctrl" 
		"rotateX" " -av"
		2 "|box_character_rig_01:chara_cube_main_ctrl|box_character_rig_01:chara_cube_footL_ctrl" 
		"rotateY" " -av"
		2 "|box_character_rig_01:chara_cube_main_ctrl|box_character_rig_01:chara_cube_footL_ctrl" 
		"rotateZ" " -av"
		2 "|box_character_rig_01:chara_cube_main_ctrl|box_character_rig_01:chara_cube_footL_ctrl|box_character_rig_01:chara_cube_footL_geo|box_character_rig_01:chara_cube_footL_geoShape" 
		"dispResolution" " 1"
		2 "|box_character_rig_01:chara_cube_main_ctrl|box_character_rig_01:chara_cube_footL_ctrl|box_character_rig_01:chara_cube_footL_geo|box_character_rig_01:chara_cube_footL_geoShape" 
		"displaySmoothMesh" " 0"
		2 "|box_character_rig_01:chara_cube_main_ctrl|box_character_rig_01:chara_cube_body_ctrl" 
		"visibility" " -av 1"
		2 "|box_character_rig_01:chara_cube_main_ctrl|box_character_rig_01:chara_cube_body_ctrl" 
		"translate" " -type \"double3\" 0 0.30931451121392683 0.43304031569949686"
		2 "|box_character_rig_01:chara_cube_main_ctrl|box_character_rig_01:chara_cube_body_ctrl" 
		"translateX" " -av"
		2 "|box_character_rig_01:chara_cube_main_ctrl|box_character_rig_01:chara_cube_body_ctrl" 
		"translateY" " -av"
		2 "|box_character_rig_01:chara_cube_main_ctrl|box_character_rig_01:chara_cube_body_ctrl" 
		"translateZ" " -av"
		2 "|box_character_rig_01:chara_cube_main_ctrl|box_character_rig_01:chara_cube_body_ctrl" 
		"rotate" " -type \"double3\" 0.49490321794228709 0 0"
		2 "|box_character_rig_01:chara_cube_main_ctrl|box_character_rig_01:chara_cube_body_ctrl" 
		"rotateX" " -av"
		2 "|box_character_rig_01:chara_cube_main_ctrl|box_character_rig_01:chara_cube_body_ctrl" 
		"rotateY" " -av"
		2 "|box_character_rig_01:chara_cube_main_ctrl|box_character_rig_01:chara_cube_body_ctrl" 
		"rotateZ" " -av"
		2 "|box_character_rig_01:chara_cube_main_ctrl|box_character_rig_01:chara_cube_body_ctrl" 
		"squash" " -av -k 1 0"
		2 "|box_character_rig_01:chara_cube_main_ctrl|box_character_rig_01:chara_cube_body_ctrl" 
		"stretch" " -av -k 1 0"
		2 "|box_character_rig_01:chara_cube_main_ctrl|box_character_rig_01:chara_cube_body_ctrl" 
		"wiggleFB" " -av -k 1 0.046669808653030387"
		2 "|box_character_rig_01:chara_cube_main_ctrl|box_character_rig_01:chara_cube_body_ctrl" 
		"wiggleLR" " -av -k 1 0"
		2 "|box_character_rig_01:chara_cube_main_ctrl|box_character_rig_01:chara_cube_body_ctrl" 
		"wiggleUD" " -av -k 1 0"
		2 "|box_character_rig_01:chara_cube_main_ctrl|box_character_rig_01:chara_cube_body_ctrl|box_character_rig_01:chara_cube_main_geo" 
		"visibility" " 1"
		2 "|box_character_rig_01:chara_cube_main_ctrl|box_character_rig_01:chara_cube_body_ctrl|box_character_rig_01:chara_cube_main_geo|box_character_rig_01:chara_cube_main_geoShape" 
		"dispResolution" " 1"
		2 "|box_character_rig_01:chara_cube_main_ctrl|box_character_rig_01:chara_cube_body_ctrl|box_character_rig_01:chara_cube_main_geo|box_character_rig_01:chara_cube_main_geoShape" 
		"displaySmoothMesh" " 0"
		2 "|box_character_rig_01:chara_cube_main_ctrl|box_character_rig_01:chara_cube_body_ctrl|box_character_rig_01:chara_long_main_geo" 
		"visibility" " 0"
		2 "|box_character_rig_01:chara_cube_main_ctrl|box_character_rig_01:chara_cube_body_ctrl|box_character_rig_01:chara_long_main_geo|box_character_rig_01:chara_long_main_geoShape" 
		"dispResolution" " 1"
		2 "|box_character_rig_01:chara_cube_main_ctrl|box_character_rig_01:chara_cube_body_ctrl|box_character_rig_01:chara_long_main_geo|box_character_rig_01:chara_long_main_geoShape" 
		"displaySmoothMesh" " 0"
		2 "|box_character_rig_01:chara_cube_main_ctrl|box_character_rig_01:chara_cube_body_ctrl|box_character_rig_01:chara_L_main_geo" 
		"visibility" " 0"
		2 "|box_character_rig_01:chara_cube_main_ctrl|box_character_rig_01:chara_cube_body_ctrl|box_character_rig_01:chara_L_main_geo|box_character_rig_01:chara_L_main_geoShape" 
		"dispResolution" " 1"
		2 "|box_character_rig_01:chara_cube_main_ctrl|box_character_rig_01:chara_cube_body_ctrl|box_character_rig_01:chara_L_main_geo|box_character_rig_01:chara_L_main_geoShape" 
		"displaySmoothMesh" " 0"
		2 "|box_character_rig_01:chara_cube_main_ctrl|box_character_rig_01:chara_cube_body_ctrl|box_character_rig_01:chara_T_main_geo" 
		"visibility" " 0"
		2 "|box_character_rig_01:chara_cube_main_ctrl|box_character_rig_01:chara_cube_body_ctrl|box_character_rig_01:chara_T_main_geo|box_character_rig_01:chara_T_main_geoShape" 
		"dispResolution" " 1"
		2 "|box_character_rig_01:chara_cube_main_ctrl|box_character_rig_01:chara_cube_body_ctrl|box_character_rig_01:chara_T_main_geo|box_character_rig_01:chara_T_main_geoShape" 
		"displaySmoothMesh" " 0"
		2 "box_character_rig_01:BaseAnimation" "preferred" " 0"
		2 "box_character_rig_01:BaseAnimation" "selected" " 0"
		2 "box_character_rig_01:cube_BS" "envelope" " 1"
		2 "box_character_rig_01:cube_BS" "midLayerParent" " 0"
		2 "box_character_rig_01:longPiece_BS" "envelope" " 1"
		2 "box_character_rig_01:longPiece_BS" "midLayerParent" " 0"
		2 "box_character_rig_01:LPiece_BS" "envelope" " 1"
		2 "box_character_rig_01:LPiece_BS" "midLayerParent" " 0"
		2 "box_character_rig_01:TPiece_BS" "envelope" " 1"
		2 "box_character_rig_01:TPiece_BS" "midLayerParent" " 0"
		5 4 "box_character_rig_01RN" "|box_character_rig_01:chara_cube_main_ctrl.translateX" 
		"box_character_rig_01RN.placeHolderList[1]" ""
		5 4 "box_character_rig_01RN" "|box_character_rig_01:chara_cube_main_ctrl.translateY" 
		"box_character_rig_01RN.placeHolderList[2]" ""
		5 4 "box_character_rig_01RN" "|box_character_rig_01:chara_cube_main_ctrl.translateZ" 
		"box_character_rig_01RN.placeHolderList[3]" ""
		5 4 "box_character_rig_01RN" "|box_character_rig_01:chara_cube_main_ctrl.visibility" 
		"box_character_rig_01RN.placeHolderList[4]" ""
		5 4 "box_character_rig_01RN" "|box_character_rig_01:chara_cube_main_ctrl.rotateX" 
		"box_character_rig_01RN.placeHolderList[5]" ""
		5 4 "box_character_rig_01RN" "|box_character_rig_01:chara_cube_main_ctrl.rotateY" 
		"box_character_rig_01RN.placeHolderList[6]" ""
		5 4 "box_character_rig_01RN" "|box_character_rig_01:chara_cube_main_ctrl.rotateZ" 
		"box_character_rig_01RN.placeHolderList[7]" ""
		5 4 "box_character_rig_01RN" "|box_character_rig_01:chara_cube_main_ctrl.scaleX" 
		"box_character_rig_01RN.placeHolderList[8]" ""
		5 4 "box_character_rig_01RN" "|box_character_rig_01:chara_cube_main_ctrl.scaleY" 
		"box_character_rig_01RN.placeHolderList[9]" ""
		5 4 "box_character_rig_01RN" "|box_character_rig_01:chara_cube_main_ctrl.scaleZ" 
		"box_character_rig_01RN.placeHolderList[10]" ""
		5 4 "box_character_rig_01RN" "|box_character_rig_01:chara_cube_main_ctrl|box_character_rig_01:chara_cube_footR_ctrl.visibility" 
		"box_character_rig_01RN.placeHolderList[11]" ""
		5 4 "box_character_rig_01RN" "|box_character_rig_01:chara_cube_main_ctrl|box_character_rig_01:chara_cube_footR_ctrl.translateX" 
		"box_character_rig_01RN.placeHolderList[12]" ""
		5 4 "box_character_rig_01RN" "|box_character_rig_01:chara_cube_main_ctrl|box_character_rig_01:chara_cube_footR_ctrl.translateY" 
		"box_character_rig_01RN.placeHolderList[13]" ""
		5 4 "box_character_rig_01RN" "|box_character_rig_01:chara_cube_main_ctrl|box_character_rig_01:chara_cube_footR_ctrl.translateZ" 
		"box_character_rig_01RN.placeHolderList[14]" ""
		5 4 "box_character_rig_01RN" "|box_character_rig_01:chara_cube_main_ctrl|box_character_rig_01:chara_cube_footR_ctrl.rotateX" 
		"box_character_rig_01RN.placeHolderList[15]" ""
		5 4 "box_character_rig_01RN" "|box_character_rig_01:chara_cube_main_ctrl|box_character_rig_01:chara_cube_footR_ctrl.rotateY" 
		"box_character_rig_01RN.placeHolderList[16]" ""
		5 4 "box_character_rig_01RN" "|box_character_rig_01:chara_cube_main_ctrl|box_character_rig_01:chara_cube_footR_ctrl.rotateZ" 
		"box_character_rig_01RN.placeHolderList[17]" ""
		5 4 "box_character_rig_01RN" "|box_character_rig_01:chara_cube_main_ctrl|box_character_rig_01:chara_cube_footR_ctrl.scaleX" 
		"box_character_rig_01RN.placeHolderList[18]" ""
		5 4 "box_character_rig_01RN" "|box_character_rig_01:chara_cube_main_ctrl|box_character_rig_01:chara_cube_footR_ctrl.scaleY" 
		"box_character_rig_01RN.placeHolderList[19]" ""
		5 4 "box_character_rig_01RN" "|box_character_rig_01:chara_cube_main_ctrl|box_character_rig_01:chara_cube_footR_ctrl.scaleZ" 
		"box_character_rig_01RN.placeHolderList[20]" ""
		5 4 "box_character_rig_01RN" "|box_character_rig_01:chara_cube_main_ctrl|box_character_rig_01:chara_cube_footL_ctrl.visibility" 
		"box_character_rig_01RN.placeHolderList[21]" ""
		5 4 "box_character_rig_01RN" "|box_character_rig_01:chara_cube_main_ctrl|box_character_rig_01:chara_cube_footL_ctrl.translateX" 
		"box_character_rig_01RN.placeHolderList[22]" ""
		5 4 "box_character_rig_01RN" "|box_character_rig_01:chara_cube_main_ctrl|box_character_rig_01:chara_cube_footL_ctrl.translateY" 
		"box_character_rig_01RN.placeHolderList[23]" ""
		5 4 "box_character_rig_01RN" "|box_character_rig_01:chara_cube_main_ctrl|box_character_rig_01:chara_cube_footL_ctrl.translateZ" 
		"box_character_rig_01RN.placeHolderList[24]" ""
		5 4 "box_character_rig_01RN" "|box_character_rig_01:chara_cube_main_ctrl|box_character_rig_01:chara_cube_footL_ctrl.rotateX" 
		"box_character_rig_01RN.placeHolderList[25]" ""
		5 4 "box_character_rig_01RN" "|box_character_rig_01:chara_cube_main_ctrl|box_character_rig_01:chara_cube_footL_ctrl.rotateY" 
		"box_character_rig_01RN.placeHolderList[26]" ""
		5 4 "box_character_rig_01RN" "|box_character_rig_01:chara_cube_main_ctrl|box_character_rig_01:chara_cube_footL_ctrl.rotateZ" 
		"box_character_rig_01RN.placeHolderList[27]" ""
		5 4 "box_character_rig_01RN" "|box_character_rig_01:chara_cube_main_ctrl|box_character_rig_01:chara_cube_footL_ctrl.scaleX" 
		"box_character_rig_01RN.placeHolderList[28]" ""
		5 4 "box_character_rig_01RN" "|box_character_rig_01:chara_cube_main_ctrl|box_character_rig_01:chara_cube_footL_ctrl.scaleY" 
		"box_character_rig_01RN.placeHolderList[29]" ""
		5 4 "box_character_rig_01RN" "|box_character_rig_01:chara_cube_main_ctrl|box_character_rig_01:chara_cube_footL_ctrl.scaleZ" 
		"box_character_rig_01RN.placeHolderList[30]" ""
		5 4 "box_character_rig_01RN" "|box_character_rig_01:chara_cube_main_ctrl|box_character_rig_01:chara_cube_body_ctrl.squash" 
		"box_character_rig_01RN.placeHolderList[31]" ""
		5 4 "box_character_rig_01RN" "|box_character_rig_01:chara_cube_main_ctrl|box_character_rig_01:chara_cube_body_ctrl.stretch" 
		"box_character_rig_01RN.placeHolderList[32]" ""
		5 4 "box_character_rig_01RN" "|box_character_rig_01:chara_cube_main_ctrl|box_character_rig_01:chara_cube_body_ctrl.wiggleFB" 
		"box_character_rig_01RN.placeHolderList[33]" ""
		5 4 "box_character_rig_01RN" "|box_character_rig_01:chara_cube_main_ctrl|box_character_rig_01:chara_cube_body_ctrl.wiggleLR" 
		"box_character_rig_01RN.placeHolderList[34]" ""
		5 4 "box_character_rig_01RN" "|box_character_rig_01:chara_cube_main_ctrl|box_character_rig_01:chara_cube_body_ctrl.wiggleUD" 
		"box_character_rig_01RN.placeHolderList[35]" ""
		5 4 "box_character_rig_01RN" "|box_character_rig_01:chara_cube_main_ctrl|box_character_rig_01:chara_cube_body_ctrl.visibility" 
		"box_character_rig_01RN.placeHolderList[36]" ""
		5 4 "box_character_rig_01RN" "|box_character_rig_01:chara_cube_main_ctrl|box_character_rig_01:chara_cube_body_ctrl.translateX" 
		"box_character_rig_01RN.placeHolderList[37]" ""
		5 4 "box_character_rig_01RN" "|box_character_rig_01:chara_cube_main_ctrl|box_character_rig_01:chara_cube_body_ctrl.translateY" 
		"box_character_rig_01RN.placeHolderList[38]" ""
		5 4 "box_character_rig_01RN" "|box_character_rig_01:chara_cube_main_ctrl|box_character_rig_01:chara_cube_body_ctrl.translateZ" 
		"box_character_rig_01RN.placeHolderList[39]" ""
		5 4 "box_character_rig_01RN" "|box_character_rig_01:chara_cube_main_ctrl|box_character_rig_01:chara_cube_body_ctrl.rotateX" 
		"box_character_rig_01RN.placeHolderList[40]" ""
		5 4 "box_character_rig_01RN" "|box_character_rig_01:chara_cube_main_ctrl|box_character_rig_01:chara_cube_body_ctrl.rotateY" 
		"box_character_rig_01RN.placeHolderList[41]" ""
		5 4 "box_character_rig_01RN" "|box_character_rig_01:chara_cube_main_ctrl|box_character_rig_01:chara_cube_body_ctrl.rotateZ" 
		"box_character_rig_01RN.placeHolderList[42]" "";
	setAttr ".ptag" -type "string" "";
lockNode -l 1 ;
createNode animLayer -n "BaseAnimation";
	rename -uid "F2D4FF54-4F69-881C-CBB5-54B5B17C9927";
	setAttr ".ovrd" yes;
createNode script -n "uiConfigurationScriptNode";
	rename -uid "D20E7339-4C2F-B294-DF37-F4B1B00E5CBF";
	setAttr ".b" -type "string" (
		"// Maya Mel UI Configuration File.\n//\n//  This script is machine generated.  Edit at your own risk.\n//\n//\n\nglobal string $gMainPane;\nif (`paneLayout -exists $gMainPane`) {\n\n\tglobal int $gUseScenePanelConfig;\n\tint    $useSceneConfig = $gUseScenePanelConfig;\n\tint    $nodeEditorPanelVisible = stringArrayContains(\"nodeEditorPanel1\", `getPanel -vis`);\n\tint    $nodeEditorWorkspaceControlOpen = (`workspaceControl -exists nodeEditorPanel1Window` && `workspaceControl -q -visible nodeEditorPanel1Window`);\n\tint    $menusOkayInPanels = `optionVar -q allowMenusInPanels`;\n\tint    $nVisPanes = `paneLayout -q -nvp $gMainPane`;\n\tint    $nPanes = 0;\n\tstring $editorName;\n\tstring $panelName;\n\tstring $itemFilterName;\n\tstring $panelConfig;\n\n\t//\n\t//  get current state of the UI\n\t//\n\tsceneUIReplacement -update $gMainPane;\n\n\t$panelName = `sceneUIReplacement -getNextPanel \"modelPanel\" (localizedPanelLabel(\"Top View\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tmodelPanel -edit -l (localizedPanelLabel(\"Top View\")) -mbv $menusOkayInPanels  $panelName;\n"
		+ "\t\t$editorName = $panelName;\n        modelEditor -e \n            -camera \"top\" \n            -useInteractiveMode 0\n            -displayLights \"default\" \n            -displayAppearance \"smoothShaded\" \n            -activeOnly 0\n            -ignorePanZoom 0\n            -wireframeOnShaded 0\n            -headsUpDisplay 1\n            -holdOuts 1\n            -selectionHiliteDisplay 1\n            -useDefaultMaterial 0\n            -bufferMode \"double\" \n            -twoSidedLighting 0\n            -backfaceCulling 0\n            -xray 0\n            -jointXray 0\n            -activeComponentsXray 0\n            -displayTextures 0\n            -smoothWireframe 0\n            -lineWidth 1\n            -textureAnisotropic 0\n            -textureHilight 1\n            -textureSampling 2\n            -textureDisplay \"modulate\" \n            -textureMaxSize 32768\n            -fogging 0\n            -fogSource \"fragment\" \n            -fogMode \"linear\" \n            -fogStart 0\n            -fogEnd 100\n            -fogDensity 0.1\n            -fogColor 0.5 0.5 0.5 1 \n"
		+ "            -depthOfFieldPreview 1\n            -maxConstantTransparency 1\n            -rendererName \"vp2Renderer\" \n            -objectFilterShowInHUD 1\n            -isFiltered 0\n            -colorResolution 256 256 \n            -bumpResolution 512 512 \n            -textureCompression 0\n            -transparencyAlgorithm \"frontAndBackCull\" \n            -transpInShadows 0\n            -cullingOverride \"none\" \n            -lowQualityLighting 0\n            -maximumNumHardwareLights 1\n            -occlusionCulling 0\n            -shadingModel 0\n            -useBaseRenderer 0\n            -useReducedRenderer 0\n            -smallObjectCulling 0\n            -smallObjectThreshold -1 \n            -interactiveDisableShadows 0\n            -interactiveBackFaceCull 0\n            -sortTransparent 1\n            -controllers 1\n            -nurbsCurves 1\n            -nurbsSurfaces 1\n            -polymeshes 1\n            -subdivSurfaces 1\n            -planes 1\n            -lights 1\n            -cameras 1\n            -controlVertices 1\n"
		+ "            -hulls 1\n            -grid 1\n            -imagePlane 1\n            -joints 1\n            -ikHandles 1\n            -deformers 1\n            -dynamics 1\n            -particleInstancers 1\n            -fluids 1\n            -hairSystems 1\n            -follicles 1\n            -nCloths 1\n            -nParticles 1\n            -nRigids 1\n            -dynamicConstraints 1\n            -locators 1\n            -manipulators 1\n            -pluginShapes 1\n            -dimensions 1\n            -handles 1\n            -pivots 1\n            -textures 1\n            -strokes 1\n            -motionTrails 1\n            -clipGhosts 1\n            -greasePencils 1\n            -shadows 0\n            -captureSequenceNumber -1\n            -width 1\n            -height 1\n            -sceneRenderFilter 0\n            $editorName;\n        modelEditor -e -viewSelected 0 $editorName;\n        modelEditor -e \n            -pluginObjects \"gpuCacheDisplayFilter\" 1 \n            $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n"
		+ "\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextPanel \"modelPanel\" (localizedPanelLabel(\"Side View\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tmodelPanel -edit -l (localizedPanelLabel(\"Side View\")) -mbv $menusOkayInPanels  $panelName;\n\t\t$editorName = $panelName;\n        modelEditor -e \n            -docTag \"RADRENDER\" \n            -camera \"side\" \n            -useInteractiveMode 0\n            -displayLights \"default\" \n            -displayAppearance \"smoothShaded\" \n            -activeOnly 0\n            -ignorePanZoom 0\n            -wireframeOnShaded 0\n            -headsUpDisplay 1\n            -holdOuts 1\n            -selectionHiliteDisplay 1\n            -useDefaultMaterial 0\n            -bufferMode \"double\" \n            -twoSidedLighting 0\n            -backfaceCulling 0\n            -xray 0\n            -jointXray 0\n            -activeComponentsXray 0\n            -displayTextures 0\n            -smoothWireframe 0\n            -lineWidth 1\n            -textureAnisotropic 0\n            -textureHilight 1\n"
		+ "            -textureSampling 2\n            -textureDisplay \"modulate\" \n            -textureMaxSize 32768\n            -fogging 0\n            -fogSource \"fragment\" \n            -fogMode \"linear\" \n            -fogStart 0\n            -fogEnd 100\n            -fogDensity 0.1\n            -fogColor 0.5 0.5 0.5 1 \n            -depthOfFieldPreview 1\n            -maxConstantTransparency 1\n            -rendererName \"vp2Renderer\" \n            -objectFilterShowInHUD 1\n            -isFiltered 0\n            -colorResolution 256 256 \n            -bumpResolution 512 512 \n            -textureCompression 0\n            -transparencyAlgorithm \"frontAndBackCull\" \n            -transpInShadows 0\n            -cullingOverride \"none\" \n            -lowQualityLighting 0\n            -maximumNumHardwareLights 1\n            -occlusionCulling 0\n            -shadingModel 0\n            -useBaseRenderer 0\n            -useReducedRenderer 0\n            -smallObjectCulling 0\n            -smallObjectThreshold -1 \n            -interactiveDisableShadows 0\n"
		+ "            -interactiveBackFaceCull 0\n            -sortTransparent 1\n            -controllers 1\n            -nurbsCurves 1\n            -nurbsSurfaces 1\n            -polymeshes 1\n            -subdivSurfaces 1\n            -planes 1\n            -lights 1\n            -cameras 1\n            -controlVertices 1\n            -hulls 1\n            -grid 1\n            -imagePlane 1\n            -joints 1\n            -ikHandles 1\n            -deformers 1\n            -dynamics 1\n            -particleInstancers 1\n            -fluids 1\n            -hairSystems 1\n            -follicles 1\n            -nCloths 1\n            -nParticles 1\n            -nRigids 1\n            -dynamicConstraints 1\n            -locators 1\n            -manipulators 1\n            -pluginShapes 1\n            -dimensions 1\n            -handles 1\n            -pivots 1\n            -textures 1\n            -strokes 1\n            -motionTrails 1\n            -clipGhosts 1\n            -greasePencils 1\n            -shadows 0\n            -captureSequenceNumber -1\n"
		+ "            -width 1\n            -height 1\n            -sceneRenderFilter 0\n            $editorName;\n        modelEditor -e -viewSelected 0 $editorName;\n        modelEditor -e \n            -pluginObjects \"gpuCacheDisplayFilter\" 1 \n            $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextPanel \"modelPanel\" (localizedPanelLabel(\"Front View\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tmodelPanel -edit -l (localizedPanelLabel(\"Front View\")) -mbv $menusOkayInPanels  $panelName;\n\t\t$editorName = $panelName;\n        modelEditor -e \n            -camera \"front\" \n            -useInteractiveMode 0\n            -displayLights \"default\" \n            -displayAppearance \"smoothShaded\" \n            -activeOnly 0\n            -ignorePanZoom 0\n            -wireframeOnShaded 0\n            -headsUpDisplay 1\n            -holdOuts 1\n            -selectionHiliteDisplay 1\n            -useDefaultMaterial 0\n            -bufferMode \"double\" \n"
		+ "            -twoSidedLighting 0\n            -backfaceCulling 0\n            -xray 0\n            -jointXray 0\n            -activeComponentsXray 0\n            -displayTextures 0\n            -smoothWireframe 0\n            -lineWidth 1\n            -textureAnisotropic 0\n            -textureHilight 1\n            -textureSampling 2\n            -textureDisplay \"modulate\" \n            -textureMaxSize 32768\n            -fogging 0\n            -fogSource \"fragment\" \n            -fogMode \"linear\" \n            -fogStart 0\n            -fogEnd 100\n            -fogDensity 0.1\n            -fogColor 0.5 0.5 0.5 1 \n            -depthOfFieldPreview 1\n            -maxConstantTransparency 1\n            -rendererName \"vp2Renderer\" \n            -objectFilterShowInHUD 1\n            -isFiltered 0\n            -colorResolution 256 256 \n            -bumpResolution 512 512 \n            -textureCompression 0\n            -transparencyAlgorithm \"frontAndBackCull\" \n            -transpInShadows 0\n            -cullingOverride \"none\" \n            -lowQualityLighting 0\n"
		+ "            -maximumNumHardwareLights 1\n            -occlusionCulling 0\n            -shadingModel 0\n            -useBaseRenderer 0\n            -useReducedRenderer 0\n            -smallObjectCulling 0\n            -smallObjectThreshold -1 \n            -interactiveDisableShadows 0\n            -interactiveBackFaceCull 0\n            -sortTransparent 1\n            -controllers 1\n            -nurbsCurves 1\n            -nurbsSurfaces 1\n            -polymeshes 1\n            -subdivSurfaces 1\n            -planes 1\n            -lights 1\n            -cameras 1\n            -controlVertices 1\n            -hulls 1\n            -grid 1\n            -imagePlane 1\n            -joints 1\n            -ikHandles 1\n            -deformers 1\n            -dynamics 1\n            -particleInstancers 1\n            -fluids 1\n            -hairSystems 1\n            -follicles 1\n            -nCloths 1\n            -nParticles 1\n            -nRigids 1\n            -dynamicConstraints 1\n            -locators 1\n            -manipulators 1\n            -pluginShapes 1\n"
		+ "            -dimensions 1\n            -handles 1\n            -pivots 1\n            -textures 1\n            -strokes 1\n            -motionTrails 1\n            -clipGhosts 1\n            -greasePencils 1\n            -shadows 0\n            -captureSequenceNumber -1\n            -width 1\n            -height 1\n            -sceneRenderFilter 0\n            $editorName;\n        modelEditor -e -viewSelected 0 $editorName;\n        modelEditor -e \n            -pluginObjects \"gpuCacheDisplayFilter\" 1 \n            $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextPanel \"modelPanel\" (localizedPanelLabel(\"Model Panel5\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tmodelPanel -edit -l (localizedPanelLabel(\"Model Panel5\")) -mbv $menusOkayInPanels  $panelName;\n\t\t$editorName = $panelName;\n        modelEditor -e \n            -camera \"persp\" \n            -useInteractiveMode 0\n            -displayLights \"default\" \n            -displayAppearance \"smoothShaded\" \n"
		+ "            -activeOnly 0\n            -ignorePanZoom 0\n            -wireframeOnShaded 0\n            -headsUpDisplay 1\n            -holdOuts 1\n            -selectionHiliteDisplay 1\n            -useDefaultMaterial 0\n            -bufferMode \"double\" \n            -twoSidedLighting 1\n            -backfaceCulling 0\n            -xray 0\n            -jointXray 1\n            -activeComponentsXray 0\n            -displayTextures 0\n            -smoothWireframe 0\n            -lineWidth 1\n            -textureAnisotropic 0\n            -textureHilight 1\n            -textureSampling 2\n            -textureDisplay \"modulate\" \n            -textureMaxSize 32768\n            -fogging 0\n            -fogSource \"fragment\" \n            -fogMode \"linear\" \n            -fogStart 0\n            -fogEnd 100\n            -fogDensity 0.1\n            -fogColor 0.5 0.5 0.5 1 \n            -depthOfFieldPreview 1\n            -maxConstantTransparency 1\n            -rendererName \"vp2Renderer\" \n            -objectFilterShowInHUD 1\n            -isFiltered 0\n"
		+ "            -colorResolution 256 256 \n            -bumpResolution 512 512 \n            -textureCompression 0\n            -transparencyAlgorithm \"frontAndBackCull\" \n            -transpInShadows 0\n            -cullingOverride \"none\" \n            -lowQualityLighting 0\n            -maximumNumHardwareLights 0\n            -occlusionCulling 0\n            -shadingModel 0\n            -useBaseRenderer 0\n            -useReducedRenderer 0\n            -smallObjectCulling 0\n            -smallObjectThreshold -1 \n            -interactiveDisableShadows 0\n            -interactiveBackFaceCull 0\n            -sortTransparent 1\n            -controllers 1\n            -nurbsCurves 1\n            -nurbsSurfaces 1\n            -polymeshes 1\n            -subdivSurfaces 1\n            -planes 1\n            -lights 1\n            -cameras 1\n            -controlVertices 1\n            -hulls 1\n            -grid 0\n            -imagePlane 1\n            -joints 1\n            -ikHandles 1\n            -deformers 1\n            -dynamics 1\n            -particleInstancers 1\n"
		+ "            -fluids 1\n            -hairSystems 1\n            -follicles 1\n            -nCloths 1\n            -nParticles 1\n            -nRigids 1\n            -dynamicConstraints 1\n            -locators 1\n            -manipulators 1\n            -pluginShapes 1\n            -dimensions 1\n            -handles 1\n            -pivots 1\n            -textures 1\n            -strokes 1\n            -motionTrails 1\n            -clipGhosts 1\n            -greasePencils 1\n            -shadows 0\n            -captureSequenceNumber -1\n            -width 878\n            -height 1055\n            -sceneRenderFilter 0\n            $editorName;\n        modelEditor -e -viewSelected 0 $editorName;\n        modelEditor -e \n            -pluginObjects \"gpuCacheDisplayFilter\" 1 \n            $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextPanel \"outlinerPanel\" (localizedPanelLabel(\"ToggledOutliner\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n"
		+ "\t\toutlinerPanel -edit -l (localizedPanelLabel(\"ToggledOutliner\")) -mbv $menusOkayInPanels  $panelName;\n\t\t$editorName = $panelName;\n        outlinerEditor -e \n            -docTag \"isolOutln_fromSeln\" \n            -showShapes 0\n            -showAssignedMaterials 0\n            -showTimeEditor 1\n            -showReferenceNodes 1\n            -showReferenceMembers 1\n            -showAttributes 0\n            -showConnected 0\n            -showAnimCurvesOnly 0\n            -showMuteInfo 0\n            -organizeByLayer 1\n            -organizeByClip 1\n            -showAnimLayerWeight 1\n            -autoExpandLayers 1\n            -autoExpand 0\n            -showDagOnly 1\n            -showAssets 1\n            -showContainedOnly 1\n            -showPublishedAsConnected 0\n            -showParentContainers 0\n            -showContainerContents 1\n            -ignoreDagHierarchy 0\n            -expandConnections 0\n            -showUpstreamCurves 1\n            -showUnitlessCurves 1\n            -showCompounds 1\n            -showLeafs 1\n"
		+ "            -showNumericAttrsOnly 0\n            -highlightActive 1\n            -autoSelectNewObjects 0\n            -doNotSelectNewObjects 0\n            -dropIsParent 1\n            -transmitFilters 0\n            -setFilter \"defaultSetFilter\" \n            -showSetMembers 1\n            -allowMultiSelection 1\n            -alwaysToggleSelect 0\n            -directSelect 0\n            -isSet 0\n            -isSetMember 0\n            -displayMode \"DAG\" \n            -expandObjects 0\n            -setsIgnoreFilters 1\n            -containersIgnoreFilters 0\n            -editAttrName 0\n            -showAttrValues 0\n            -highlightSecondary 0\n            -showUVAttrsOnly 0\n            -showTextureNodesOnly 0\n            -attrAlphaOrder \"default\" \n            -animLayerFilterOptions \"allAffecting\" \n            -sortOrder \"none\" \n            -longNames 0\n            -niceNames 1\n            -selectCommand \"print(\\\"\\\")\" \n            -showNamespace 1\n            -showPinIcons 0\n            -mapMotionTrails 0\n            -ignoreHiddenAttribute 0\n"
		+ "            -ignoreOutlinerColor 0\n            -renderFilterVisible 0\n            -renderFilterIndex 0\n            -selectionOrder \"chronological\" \n            -expandAttribute 0\n            $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextPanel \"outlinerPanel\" (localizedPanelLabel(\"Outliner\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\toutlinerPanel -edit -l (localizedPanelLabel(\"Outliner\")) -mbv $menusOkayInPanels  $panelName;\n\t\t$editorName = $panelName;\n        outlinerEditor -e \n            -docTag \"isolOutln_fromSeln\" \n            -showShapes 0\n            -showAssignedMaterials 0\n            -showTimeEditor 1\n            -showReferenceNodes 0\n            -showReferenceMembers 0\n            -showAttributes 0\n            -showConnected 0\n            -showAnimCurvesOnly 0\n            -showMuteInfo 0\n            -organizeByLayer 1\n            -organizeByClip 1\n            -showAnimLayerWeight 1\n            -autoExpandLayers 1\n"
		+ "            -autoExpand 0\n            -showDagOnly 1\n            -showAssets 1\n            -showContainedOnly 1\n            -showPublishedAsConnected 0\n            -showParentContainers 0\n            -showContainerContents 1\n            -ignoreDagHierarchy 0\n            -expandConnections 0\n            -showUpstreamCurves 1\n            -showUnitlessCurves 1\n            -showCompounds 1\n            -showLeafs 1\n            -showNumericAttrsOnly 0\n            -highlightActive 1\n            -autoSelectNewObjects 0\n            -doNotSelectNewObjects 0\n            -dropIsParent 1\n            -transmitFilters 0\n            -setFilter \"defaultSetFilter\" \n            -showSetMembers 1\n            -allowMultiSelection 1\n            -alwaysToggleSelect 0\n            -directSelect 0\n            -displayMode \"DAG\" \n            -expandObjects 0\n            -setsIgnoreFilters 1\n            -containersIgnoreFilters 0\n            -editAttrName 0\n            -showAttrValues 0\n            -highlightSecondary 0\n            -showUVAttrsOnly 0\n"
		+ "            -showTextureNodesOnly 0\n            -attrAlphaOrder \"default\" \n            -animLayerFilterOptions \"allAffecting\" \n            -sortOrder \"none\" \n            -longNames 0\n            -niceNames 1\n            -showNamespace 1\n            -showPinIcons 0\n            -mapMotionTrails 0\n            -ignoreHiddenAttribute 0\n            -ignoreOutlinerColor 0\n            -renderFilterVisible 0\n            $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"graphEditor\" (localizedPanelLabel(\"Graph Editor\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Graph Editor\")) -mbv $menusOkayInPanels  $panelName;\n\n\t\t\t$editorName = ($panelName+\"OutlineEd\");\n            outlinerEditor -e \n                -showShapes 1\n                -showAssignedMaterials 0\n                -showTimeEditor 1\n                -showReferenceNodes 0\n                -showReferenceMembers 0\n"
		+ "                -showAttributes 1\n                -showConnected 1\n                -showAnimCurvesOnly 1\n                -showMuteInfo 0\n                -organizeByLayer 1\n                -organizeByClip 1\n                -showAnimLayerWeight 1\n                -autoExpandLayers 1\n                -autoExpand 1\n                -showDagOnly 0\n                -showAssets 1\n                -showContainedOnly 0\n                -showPublishedAsConnected 0\n                -showParentContainers 1\n                -showContainerContents 0\n                -ignoreDagHierarchy 0\n                -expandConnections 1\n                -showUpstreamCurves 1\n                -showUnitlessCurves 1\n                -showCompounds 0\n                -showLeafs 1\n                -showNumericAttrsOnly 1\n                -highlightActive 0\n                -autoSelectNewObjects 1\n                -doNotSelectNewObjects 0\n                -dropIsParent 1\n                -transmitFilters 1\n                -setFilter \"0\" \n                -showSetMembers 0\n"
		+ "                -allowMultiSelection 1\n                -alwaysToggleSelect 0\n                -directSelect 0\n                -isSet 0\n                -isSetMember 0\n                -displayMode \"DAG\" \n                -expandObjects 0\n                -setsIgnoreFilters 1\n                -containersIgnoreFilters 0\n                -editAttrName 0\n                -showAttrValues 0\n                -highlightSecondary 0\n                -showUVAttrsOnly 0\n                -showTextureNodesOnly 0\n                -attrAlphaOrder \"default\" \n                -animLayerFilterOptions \"allAffecting\" \n                -sortOrder \"none\" \n                -longNames 0\n                -niceNames 1\n                -showNamespace 1\n                -showPinIcons 1\n                -mapMotionTrails 1\n                -ignoreHiddenAttribute 0\n                -ignoreOutlinerColor 0\n                -renderFilterVisible 0\n                -selectionOrder \"display\" \n                -expandAttribute 1\n                $editorName;\n\n\t\t\t$editorName = ($panelName+\"GraphEd\");\n"
		+ "            animCurveEditor -e \n                -displayKeys 1\n                -displayTangents 0\n                -displayActiveKeys 0\n                -displayActiveKeyTangents 1\n                -displayInfinities 0\n                -displayValues 0\n                -autoFit 0\n                -autoFitTime 0\n                -snapTime \"integer\" \n                -snapValue \"none\" \n                -showResults \"off\" \n                -showBufferCurves \"off\" \n                -smoothness \"fine\" \n                -resultSamples 1\n                -resultScreenSamples 0\n                -resultUpdate \"delayed\" \n                -showUpstreamCurves 1\n                -showCurveNames 0\n                -showActiveCurveNames 0\n                -clipTime \"on\" \n                -stackedCurves 0\n                -stackedCurvesMin -1\n                -stackedCurvesMax 1\n                -stackedCurvesSpace 0.2\n                -displayNormalized 0\n                -preSelectionHighlight 0\n                -constrainDrag 0\n                -classicMode 1\n"
		+ "                -valueLinesToggle 0\n                -outliner \"graphEditor1OutlineEd\" \n                $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"dopeSheetPanel\" (localizedPanelLabel(\"Dope Sheet\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Dope Sheet\")) -mbv $menusOkayInPanels  $panelName;\n\n\t\t\t$editorName = ($panelName+\"OutlineEd\");\n            outlinerEditor -e \n                -showShapes 1\n                -showAssignedMaterials 0\n                -showTimeEditor 1\n                -showReferenceNodes 0\n                -showReferenceMembers 0\n                -showAttributes 1\n                -showConnected 1\n                -showAnimCurvesOnly 1\n                -showMuteInfo 0\n                -organizeByLayer 1\n                -organizeByClip 1\n                -showAnimLayerWeight 1\n                -autoExpandLayers 1\n                -autoExpand 0\n"
		+ "                -showDagOnly 0\n                -showAssets 1\n                -showContainedOnly 0\n                -showPublishedAsConnected 0\n                -showParentContainers 1\n                -showContainerContents 0\n                -ignoreDagHierarchy 0\n                -expandConnections 1\n                -showUpstreamCurves 1\n                -showUnitlessCurves 0\n                -showCompounds 1\n                -showLeafs 1\n                -showNumericAttrsOnly 1\n                -highlightActive 0\n                -autoSelectNewObjects 0\n                -doNotSelectNewObjects 1\n                -dropIsParent 1\n                -transmitFilters 0\n                -setFilter \"0\" \n                -showSetMembers 0\n                -allowMultiSelection 1\n                -alwaysToggleSelect 0\n                -directSelect 0\n                -displayMode \"DAG\" \n                -expandObjects 0\n                -setsIgnoreFilters 1\n                -containersIgnoreFilters 0\n                -editAttrName 0\n                -showAttrValues 0\n"
		+ "                -highlightSecondary 0\n                -showUVAttrsOnly 0\n                -showTextureNodesOnly 0\n                -attrAlphaOrder \"default\" \n                -animLayerFilterOptions \"allAffecting\" \n                -sortOrder \"none\" \n                -longNames 0\n                -niceNames 1\n                -showNamespace 1\n                -showPinIcons 0\n                -mapMotionTrails 1\n                -ignoreHiddenAttribute 0\n                -ignoreOutlinerColor 0\n                -renderFilterVisible 0\n                $editorName;\n\n\t\t\t$editorName = ($panelName+\"DopeSheetEd\");\n            dopeSheetEditor -e \n                -displayKeys 1\n                -displayTangents 0\n                -displayActiveKeys 0\n                -displayActiveKeyTangents 0\n                -displayInfinities 0\n                -displayValues 0\n                -autoFit 0\n                -autoFitTime 0\n                -snapTime \"integer\" \n                -snapValue \"none\" \n                -outliner \"dopeSheetPanel1OutlineEd\" \n"
		+ "                -showSummary 1\n                -showScene 0\n                -hierarchyBelow 0\n                -showTicks 1\n                -selectionWindow 0 0 0 0 \n                $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"timeEditorPanel\" (localizedPanelLabel(\"Time Editor\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Time Editor\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"clipEditorPanel\" (localizedPanelLabel(\"Trax Editor\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Trax Editor\")) -mbv $menusOkayInPanels  $panelName;\n\n\t\t\t$editorName = clipEditorNameFromPanel($panelName);\n            clipEditor -e \n                -displayKeys 0\n                -displayTangents 0\n"
		+ "                -displayActiveKeys 0\n                -displayActiveKeyTangents 0\n                -displayInfinities 0\n                -displayValues 0\n                -autoFit 0\n                -autoFitTime 0\n                -snapTime \"none\" \n                -snapValue \"none\" \n                -initialized 0\n                -manageSequencer 0 \n                $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"sequenceEditorPanel\" (localizedPanelLabel(\"Camera Sequencer\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Camera Sequencer\")) -mbv $menusOkayInPanels  $panelName;\n\n\t\t\t$editorName = sequenceEditorNameFromPanel($panelName);\n            clipEditor -e \n                -displayKeys 0\n                -displayTangents 0\n                -displayActiveKeys 0\n                -displayActiveKeyTangents 0\n                -displayInfinities 0\n                -displayValues 0\n"
		+ "                -autoFit 0\n                -autoFitTime 0\n                -snapTime \"none\" \n                -snapValue \"none\" \n                -initialized 0\n                -manageSequencer 1 \n                $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"hyperGraphPanel\" (localizedPanelLabel(\"Hypergraph Hierarchy\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Hypergraph Hierarchy\")) -mbv $menusOkayInPanels  $panelName;\n\n\t\t\t$editorName = ($panelName+\"HyperGraphEd\");\n            hyperGraph -e \n                -graphLayoutStyle \"hierarchicalLayout\" \n                -orientation \"horiz\" \n                -mergeConnections 0\n                -zoom 1\n                -animateTransition 0\n                -showRelationships 1\n                -showShapes 0\n                -showDeformers 0\n                -showExpressions 0\n                -showConstraints 0\n                -showConnectionFromSelected 0\n"
		+ "                -showConnectionToSelected 0\n                -showConstraintLabels 0\n                -showUnderworld 0\n                -showInvisible 0\n                -transitionFrames 1\n                -opaqueContainers 0\n                -freeform 0\n                -imagePosition 0 0 \n                -imageScale 1\n                -imageEnabled 0\n                -graphType \"DAG\" \n                -heatMapDisplay 0\n                -updateSelection 1\n                -updateNodeAdded 1\n                -useDrawOverrideColor 0\n                -limitGraphTraversal -1\n                -range 0 0 \n                -iconSize \"smallIcons\" \n                -showCachedConnections 0\n                $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"hyperShadePanel\" (localizedPanelLabel(\"Hypershade\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Hypershade\")) -mbv $menusOkayInPanels  $panelName;\n"
		+ "\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"visorPanel\" (localizedPanelLabel(\"Visor\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Visor\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"nodeEditorPanel\" (localizedPanelLabel(\"Node Editor\")) `;\n\tif ($nodeEditorPanelVisible || $nodeEditorWorkspaceControlOpen) {\n\t\tif (\"\" == $panelName) {\n\t\t\tif ($useSceneConfig) {\n\t\t\t\t$panelName = `scriptedPanel -unParent  -type \"nodeEditorPanel\" -l (localizedPanelLabel(\"Node Editor\")) -mbv $menusOkayInPanels `;\n\n\t\t\t$editorName = ($panelName+\"NodeEditorEd\");\n            nodeEditor -e \n                -allAttributes 0\n                -allNodes 0\n                -autoSizeNodes 1\n                -consistentNameSize 1\n                -createNodeCommand \"nodeEdCreateNodeCommand\" \n"
		+ "                -connectNodeOnCreation 0\n                -connectOnDrop 0\n                -copyConnectionsOnPaste 0\n                -defaultPinnedState 0\n                -additiveGraphingMode 0\n                -settingsChangedCallback \"nodeEdSyncControls\" \n                -traversalDepthLimit -1\n                -keyPressCommand \"nodeEdKeyPressCommand\" \n                -nodeTitleMode \"name\" \n                -gridSnap 0\n                -gridVisibility 1\n                -crosshairOnEdgeDragging 0\n                -popupMenuScript \"nodeEdBuildPanelMenus\" \n                -showNamespace 1\n                -showShapes 1\n                -showSGShapes 0\n                -showTransforms 1\n                -useAssets 1\n                -syncedSelection 1\n                -extendToShapes 1\n                -editorMode \"default\" \n                $editorName;\n\t\t\t}\n\t\t} else {\n\t\t\t$label = `panel -q -label $panelName`;\n\t\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Node Editor\")) -mbv $menusOkayInPanels  $panelName;\n\n\t\t\t$editorName = ($panelName+\"NodeEditorEd\");\n"
		+ "            nodeEditor -e \n                -allAttributes 0\n                -allNodes 0\n                -autoSizeNodes 1\n                -consistentNameSize 1\n                -createNodeCommand \"nodeEdCreateNodeCommand\" \n                -connectNodeOnCreation 0\n                -connectOnDrop 0\n                -copyConnectionsOnPaste 0\n                -defaultPinnedState 0\n                -additiveGraphingMode 0\n                -settingsChangedCallback \"nodeEdSyncControls\" \n                -traversalDepthLimit -1\n                -keyPressCommand \"nodeEdKeyPressCommand\" \n                -nodeTitleMode \"name\" \n                -gridSnap 0\n                -gridVisibility 1\n                -crosshairOnEdgeDragging 0\n                -popupMenuScript \"nodeEdBuildPanelMenus\" \n                -showNamespace 1\n                -showShapes 1\n                -showSGShapes 0\n                -showTransforms 1\n                -useAssets 1\n                -syncedSelection 1\n                -extendToShapes 1\n                -editorMode \"default\" \n"
		+ "                $editorName;\n\t\t\tif (!$useSceneConfig) {\n\t\t\t\tpanel -e -l $label $panelName;\n\t\t\t}\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"createNodePanel\" (localizedPanelLabel(\"Create Node\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Create Node\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"polyTexturePlacementPanel\" (localizedPanelLabel(\"UV Editor\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"UV Editor\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"renderWindowPanel\" (localizedPanelLabel(\"Render View\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Render View\")) -mbv $menusOkayInPanels  $panelName;\n"
		+ "\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextPanel \"shapePanel\" (localizedPanelLabel(\"Shape Editor\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tshapePanel -edit -l (localizedPanelLabel(\"Shape Editor\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextPanel \"posePanel\" (localizedPanelLabel(\"Pose Editor\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tposePanel -edit -l (localizedPanelLabel(\"Pose Editor\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"dynRelEdPanel\" (localizedPanelLabel(\"Dynamic Relationships\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Dynamic Relationships\")) -mbv $menusOkayInPanels  $panelName;\n"
		+ "\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"relationshipPanel\" (localizedPanelLabel(\"Relationship Editor\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Relationship Editor\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"referenceEditorPanel\" (localizedPanelLabel(\"Reference Editor\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Reference Editor\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"componentEditorPanel\" (localizedPanelLabel(\"Component Editor\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Component Editor\")) -mbv $menusOkayInPanels  $panelName;\n"
		+ "\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"dynPaintScriptedPanelType\" (localizedPanelLabel(\"Paint Effects\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Paint Effects\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"scriptEditorPanel\" (localizedPanelLabel(\"Script Editor\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Script Editor\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"profilerPanel\" (localizedPanelLabel(\"Profiler Tool\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Profiler Tool\")) -mbv $menusOkayInPanels  $panelName;\n"
		+ "\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"contentBrowserPanel\" (localizedPanelLabel(\"Content Browser\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Content Browser\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"Stereo\" (localizedPanelLabel(\"Stereo\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Stereo\")) -mbv $menusOkayInPanels  $panelName;\nstring $editorName = ($panelName+\"Editor\");\n            stereoCameraView -e \n                -editorChanged \"updateModelPanelBar\" \n                -camera \"persp\" \n                -useInteractiveMode 0\n                -displayLights \"default\" \n                -displayAppearance \"smoothShaded\" \n                -activeOnly 0\n                -ignorePanZoom 0\n"
		+ "                -wireframeOnShaded 0\n                -headsUpDisplay 1\n                -holdOuts 1\n                -selectionHiliteDisplay 1\n                -useDefaultMaterial 0\n                -bufferMode \"double\" \n                -twoSidedLighting 0\n                -backfaceCulling 0\n                -xray 0\n                -jointXray 0\n                -activeComponentsXray 0\n                -displayTextures 0\n                -smoothWireframe 0\n                -lineWidth 1\n                -textureAnisotropic 0\n                -textureHilight 1\n                -textureSampling 2\n                -textureDisplay \"modulate\" \n                -textureMaxSize 32768\n                -fogging 0\n                -fogSource \"fragment\" \n                -fogMode \"linear\" \n                -fogStart 0\n                -fogEnd 100\n                -fogDensity 0.1\n                -fogColor 0.5 0.5 0.5 1 \n                -depthOfFieldPreview 1\n                -maxConstantTransparency 1\n                -rendererOverrideName \"stereoOverrideVP2\" \n"
		+ "                -objectFilterShowInHUD 1\n                -isFiltered 0\n                -colorResolution 4 4 \n                -bumpResolution 4 4 \n                -textureCompression 0\n                -transparencyAlgorithm \"frontAndBackCull\" \n                -transpInShadows 0\n                -cullingOverride \"none\" \n                -lowQualityLighting 0\n                -maximumNumHardwareLights 0\n                -occlusionCulling 0\n                -shadingModel 0\n                -useBaseRenderer 0\n                -useReducedRenderer 0\n                -smallObjectCulling 0\n                -smallObjectThreshold -1 \n                -interactiveDisableShadows 0\n                -interactiveBackFaceCull 0\n                -sortTransparent 1\n                -controllers 1\n                -nurbsCurves 1\n                -nurbsSurfaces 1\n                -polymeshes 1\n                -subdivSurfaces 1\n                -planes 1\n                -lights 1\n                -cameras 1\n                -controlVertices 1\n                -hulls 1\n"
		+ "                -grid 1\n                -imagePlane 1\n                -joints 1\n                -ikHandles 1\n                -deformers 1\n                -dynamics 1\n                -particleInstancers 1\n                -fluids 1\n                -hairSystems 1\n                -follicles 1\n                -nCloths 1\n                -nParticles 1\n                -nRigids 1\n                -dynamicConstraints 1\n                -locators 1\n                -manipulators 1\n                -pluginShapes 1\n                -dimensions 1\n                -handles 1\n                -pivots 1\n                -textures 1\n                -strokes 1\n                -motionTrails 1\n                -clipGhosts 1\n                -greasePencils 1\n                -shadows 0\n                -captureSequenceNumber -1\n                -width 0\n                -height 0\n                -sceneRenderFilter 0\n                -displayMode \"centerEye\" \n                -viewColor 0 0 0 1 \n                -useCustomBackground 1\n                $editorName;\n"
		+ "            stereoCameraView -e -viewSelected 0 $editorName;\n            stereoCameraView -e \n                -pluginObjects \"gpuCacheDisplayFilter\" 1 \n                $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextPanel \"modelPanel\" (localizedPanelLabel(\"Persp View\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tmodelPanel -edit -l (localizedPanelLabel(\"Persp View\")) -mbv $menusOkayInPanels  $panelName;\n\t\t$editorName = $panelName;\n        modelEditor -e \n            -camera \"persp\" \n            -useInteractiveMode 0\n            -displayLights \"all\" \n            -displayAppearance \"smoothShaded\" \n            -activeOnly 0\n            -ignorePanZoom 0\n            -wireframeOnShaded 0\n            -headsUpDisplay 1\n            -holdOuts 1\n            -selectionHiliteDisplay 1\n            -useDefaultMaterial 0\n            -bufferMode \"double\" \n            -twoSidedLighting 0\n            -backfaceCulling 0\n            -xray 0\n"
		+ "            -jointXray 0\n            -activeComponentsXray 0\n            -displayTextures 0\n            -smoothWireframe 0\n            -lineWidth 1\n            -textureAnisotropic 0\n            -textureHilight 1\n            -textureSampling 2\n            -textureDisplay \"modulate\" \n            -textureMaxSize 32768\n            -fogging 0\n            -fogSource \"fragment\" \n            -fogMode \"linear\" \n            -fogStart 0\n            -fogEnd 100\n            -fogDensity 0.1\n            -fogColor 0.5 0.5 0.5 1 \n            -depthOfFieldPreview 1\n            -maxConstantTransparency 1\n            -rendererName \"vp2Renderer\" \n            -objectFilterShowInHUD 1\n            -isFiltered 0\n            -colorResolution 256 256 \n            -bumpResolution 512 512 \n            -textureCompression 0\n            -transparencyAlgorithm \"frontAndBackCull\" \n            -transpInShadows 0\n            -cullingOverride \"none\" \n            -lowQualityLighting 0\n            -maximumNumHardwareLights 1\n            -occlusionCulling 0\n"
		+ "            -shadingModel 0\n            -useBaseRenderer 0\n            -useReducedRenderer 0\n            -smallObjectCulling 0\n            -smallObjectThreshold -1 \n            -interactiveDisableShadows 0\n            -interactiveBackFaceCull 0\n            -sortTransparent 1\n            -controllers 1\n            -nurbsCurves 1\n            -nurbsSurfaces 1\n            -polymeshes 1\n            -subdivSurfaces 1\n            -planes 1\n            -lights 1\n            -cameras 1\n            -controlVertices 1\n            -hulls 1\n            -grid 1\n            -imagePlane 1\n            -joints 1\n            -ikHandles 1\n            -deformers 1\n            -dynamics 1\n            -particleInstancers 1\n            -fluids 1\n            -hairSystems 1\n            -follicles 1\n            -nCloths 1\n            -nParticles 1\n            -nRigids 1\n            -dynamicConstraints 1\n            -locators 1\n            -manipulators 1\n            -pluginShapes 1\n            -dimensions 1\n            -handles 1\n            -pivots 1\n"
		+ "            -textures 1\n            -strokes 1\n            -motionTrails 1\n            -clipGhosts 1\n            -greasePencils 1\n            -shadows 1\n            -captureSequenceNumber -1\n            -width 848\n            -height 1055\n            -sceneRenderFilter 0\n            $editorName;\n        modelEditor -e -viewSelected 0 $editorName;\n        modelEditor -e \n            -pluginObjects \"gpuCacheDisplayFilter\" 1 \n            $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\tif ($useSceneConfig) {\n        string $configName = `getPanel -cwl (localizedPanelLabel(\"Current Layout\"))`;\n        if (\"\" != $configName) {\n\t\t\tpanelConfiguration -edit -label (localizedPanelLabel(\"Current Layout\")) \n\t\t\t\t-userCreated false\n\t\t\t\t-defaultImage \"vacantCell.xP:/\"\n\t\t\t\t-image \"\"\n\t\t\t\t-sc false\n\t\t\t\t-configString \"global string $gMainPane; paneLayout -e -cn \\\"single\\\" -ps 1 100 100 $gMainPane;\"\n\t\t\t\t-removeAllPanels\n\t\t\t\t-ap false\n\t\t\t\t\t(localizedPanelLabel(\"Persp View\")) \n\t\t\t\t\t\"modelPanel\"\n"
		+ "\t\t\t\t\t\"$panelName = `modelPanel -unParent -l (localizedPanelLabel(\\\"Persp View\\\")) -mbv $menusOkayInPanels `;\\n$editorName = $panelName;\\nmodelEditor -e \\n    -cam `findStartUpCamera persp` \\n    -useInteractiveMode 0\\n    -displayLights \\\"all\\\" \\n    -displayAppearance \\\"smoothShaded\\\" \\n    -activeOnly 0\\n    -ignorePanZoom 0\\n    -wireframeOnShaded 0\\n    -headsUpDisplay 1\\n    -holdOuts 1\\n    -selectionHiliteDisplay 1\\n    -useDefaultMaterial 0\\n    -bufferMode \\\"double\\\" \\n    -twoSidedLighting 0\\n    -backfaceCulling 0\\n    -xray 0\\n    -jointXray 0\\n    -activeComponentsXray 0\\n    -displayTextures 0\\n    -smoothWireframe 0\\n    -lineWidth 1\\n    -textureAnisotropic 0\\n    -textureHilight 1\\n    -textureSampling 2\\n    -textureDisplay \\\"modulate\\\" \\n    -textureMaxSize 32768\\n    -fogging 0\\n    -fogSource \\\"fragment\\\" \\n    -fogMode \\\"linear\\\" \\n    -fogStart 0\\n    -fogEnd 100\\n    -fogDensity 0.1\\n    -fogColor 0.5 0.5 0.5 1 \\n    -depthOfFieldPreview 1\\n    -maxConstantTransparency 1\\n    -rendererName \\\"vp2Renderer\\\" \\n    -objectFilterShowInHUD 1\\n    -isFiltered 0\\n    -colorResolution 256 256 \\n    -bumpResolution 512 512 \\n    -textureCompression 0\\n    -transparencyAlgorithm \\\"frontAndBackCull\\\" \\n    -transpInShadows 0\\n    -cullingOverride \\\"none\\\" \\n    -lowQualityLighting 0\\n    -maximumNumHardwareLights 1\\n    -occlusionCulling 0\\n    -shadingModel 0\\n    -useBaseRenderer 0\\n    -useReducedRenderer 0\\n    -smallObjectCulling 0\\n    -smallObjectThreshold -1 \\n    -interactiveDisableShadows 0\\n    -interactiveBackFaceCull 0\\n    -sortTransparent 1\\n    -controllers 1\\n    -nurbsCurves 1\\n    -nurbsSurfaces 1\\n    -polymeshes 1\\n    -subdivSurfaces 1\\n    -planes 1\\n    -lights 1\\n    -cameras 1\\n    -controlVertices 1\\n    -hulls 1\\n    -grid 1\\n    -imagePlane 1\\n    -joints 1\\n    -ikHandles 1\\n    -deformers 1\\n    -dynamics 1\\n    -particleInstancers 1\\n    -fluids 1\\n    -hairSystems 1\\n    -follicles 1\\n    -nCloths 1\\n    -nParticles 1\\n    -nRigids 1\\n    -dynamicConstraints 1\\n    -locators 1\\n    -manipulators 1\\n    -pluginShapes 1\\n    -dimensions 1\\n    -handles 1\\n    -pivots 1\\n    -textures 1\\n    -strokes 1\\n    -motionTrails 1\\n    -clipGhosts 1\\n    -greasePencils 1\\n    -shadows 1\\n    -captureSequenceNumber -1\\n    -width 848\\n    -height 1055\\n    -sceneRenderFilter 0\\n    $editorName;\\nmodelEditor -e -viewSelected 0 $editorName;\\nmodelEditor -e \\n    -pluginObjects \\\"gpuCacheDisplayFilter\\\" 1 \\n    $editorName\"\n"
		+ "\t\t\t\t\t\"modelPanel -edit -l (localizedPanelLabel(\\\"Persp View\\\")) -mbv $menusOkayInPanels  $panelName;\\n$editorName = $panelName;\\nmodelEditor -e \\n    -cam `findStartUpCamera persp` \\n    -useInteractiveMode 0\\n    -displayLights \\\"all\\\" \\n    -displayAppearance \\\"smoothShaded\\\" \\n    -activeOnly 0\\n    -ignorePanZoom 0\\n    -wireframeOnShaded 0\\n    -headsUpDisplay 1\\n    -holdOuts 1\\n    -selectionHiliteDisplay 1\\n    -useDefaultMaterial 0\\n    -bufferMode \\\"double\\\" \\n    -twoSidedLighting 0\\n    -backfaceCulling 0\\n    -xray 0\\n    -jointXray 0\\n    -activeComponentsXray 0\\n    -displayTextures 0\\n    -smoothWireframe 0\\n    -lineWidth 1\\n    -textureAnisotropic 0\\n    -textureHilight 1\\n    -textureSampling 2\\n    -textureDisplay \\\"modulate\\\" \\n    -textureMaxSize 32768\\n    -fogging 0\\n    -fogSource \\\"fragment\\\" \\n    -fogMode \\\"linear\\\" \\n    -fogStart 0\\n    -fogEnd 100\\n    -fogDensity 0.1\\n    -fogColor 0.5 0.5 0.5 1 \\n    -depthOfFieldPreview 1\\n    -maxConstantTransparency 1\\n    -rendererName \\\"vp2Renderer\\\" \\n    -objectFilterShowInHUD 1\\n    -isFiltered 0\\n    -colorResolution 256 256 \\n    -bumpResolution 512 512 \\n    -textureCompression 0\\n    -transparencyAlgorithm \\\"frontAndBackCull\\\" \\n    -transpInShadows 0\\n    -cullingOverride \\\"none\\\" \\n    -lowQualityLighting 0\\n    -maximumNumHardwareLights 1\\n    -occlusionCulling 0\\n    -shadingModel 0\\n    -useBaseRenderer 0\\n    -useReducedRenderer 0\\n    -smallObjectCulling 0\\n    -smallObjectThreshold -1 \\n    -interactiveDisableShadows 0\\n    -interactiveBackFaceCull 0\\n    -sortTransparent 1\\n    -controllers 1\\n    -nurbsCurves 1\\n    -nurbsSurfaces 1\\n    -polymeshes 1\\n    -subdivSurfaces 1\\n    -planes 1\\n    -lights 1\\n    -cameras 1\\n    -controlVertices 1\\n    -hulls 1\\n    -grid 1\\n    -imagePlane 1\\n    -joints 1\\n    -ikHandles 1\\n    -deformers 1\\n    -dynamics 1\\n    -particleInstancers 1\\n    -fluids 1\\n    -hairSystems 1\\n    -follicles 1\\n    -nCloths 1\\n    -nParticles 1\\n    -nRigids 1\\n    -dynamicConstraints 1\\n    -locators 1\\n    -manipulators 1\\n    -pluginShapes 1\\n    -dimensions 1\\n    -handles 1\\n    -pivots 1\\n    -textures 1\\n    -strokes 1\\n    -motionTrails 1\\n    -clipGhosts 1\\n    -greasePencils 1\\n    -shadows 1\\n    -captureSequenceNumber -1\\n    -width 848\\n    -height 1055\\n    -sceneRenderFilter 0\\n    $editorName;\\nmodelEditor -e -viewSelected 0 $editorName;\\nmodelEditor -e \\n    -pluginObjects \\\"gpuCacheDisplayFilter\\\" 1 \\n    $editorName\"\n"
		+ "\t\t\t\t$configName;\n\n            setNamedPanelLayout (localizedPanelLabel(\"Current Layout\"));\n        }\n\n        panelHistory -e -clear mainPanelHistory;\n        sceneUIReplacement -clear;\n\t}\n\n\ngrid -spacing 300 -size 300 -divisions 10 -displayAxes yes -displayGridLines yes -displayDivisionLines yes -displayPerspectiveLabels no -displayOrthographicLabels no -displayAxesBold yes -perspectiveLabelPosition axis -orthographicLabelPosition edge;\nviewManip -drawCompass 0 -compassAngle 0 -frontParameters \"\" -homeParameters \"\" -selectionLockParameters \"\";\n}\n");
	setAttr ".st" 3;
createNode script -n "sceneConfigurationScriptNode";
	rename -uid "7EA3C683-4276-8CBF-611B-BD9987440DA6";
	setAttr ".b" -type "string" "playbackOptions -min 1 -max 14 -ast 1 -aet 200 ";
	setAttr ".st" 6;
createNode animCurveTL -n "chara_cube_footL_ctrl_translateX";
	rename -uid "3609501B-4654-843C-99E8-81A28694BB65";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 3 ".ktv[0:2]"  1 35.811740499279097 7 35.811740499279097
		 10 0;
createNode animCurveTL -n "chara_cube_footL_ctrl_translateY";
	rename -uid "FCC0550F-43F5-D86C-A00D-439A8E37D36A";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 3 ".ktv[0:2]"  1 0 7 0 10 0;
createNode animCurveTL -n "chara_cube_footL_ctrl_translateZ";
	rename -uid "254DDBA2-47AB-AB99-941E-B1BD8EB29207";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 3 ".ktv[0:2]"  1 78.14741310320855 7 78.14741310320855
		 10 0;
createNode animCurveTA -n "chara_cube_footL_ctrl_rotateX";
	rename -uid "3FC30DC3-47A1-B34A-5970-24B32FBD9526";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 3 ".ktv[0:2]"  1 0 7 0 10 0;
createNode animCurveTA -n "chara_cube_footL_ctrl_rotateY";
	rename -uid "48B5FDA7-4893-123B-CAA5-3B82F3B7E100";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 3 ".ktv[0:2]"  1 12.986562057609227 7 28.628245421452164
		 10 0;
createNode animCurveTA -n "chara_cube_footL_ctrl_rotateZ";
	rename -uid "973191C2-4612-2016-E51D-A69208AC4D0F";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 3 ".ktv[0:2]"  1 0 7 0 10 0;
createNode animCurveTU -n "chara_cube_footL_ctrl_scaleX";
	rename -uid "4DF6E98F-4CAC-B357-0FD6-4EAFB1836F77";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 3 ".ktv[0:2]"  1 1 7 1 10 1;
createNode animCurveTU -n "chara_cube_footL_ctrl_scaleY";
	rename -uid "577B7AAF-41A3-9F2E-6E1A-D5875C547300";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 3 ".ktv[0:2]"  1 1 7 1 10 1;
createNode animCurveTU -n "chara_cube_footL_ctrl_scaleZ";
	rename -uid "B542CD42-44E4-B1F3-0294-7FAA844A8F7E";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 3 ".ktv[0:2]"  1 1 7 1 10 1;
createNode animCurveTL -n "chara_cube_footR_ctrl_translateX";
	rename -uid "4EE973E7-4BB7-4448-5BFB-77A60A95E7BF";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 4 ".ktv[0:3]"  1 -43.205623036446511 5 -43.205623036446511
		 7 0 10 0;
createNode animCurveTL -n "chara_cube_footR_ctrl_translateY";
	rename -uid "EC022F72-4143-4A49-85B8-A9B3D5B0C958";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 4 ".ktv[0:3]"  1 0 5 0 7 0 10 0;
createNode animCurveTL -n "chara_cube_footR_ctrl_translateZ";
	rename -uid "9941E568-4261-4537-82C6-EDBDA4F2F31B";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 4 ".ktv[0:3]"  1 78.449859972886628 5 78.449859972886628
		 7 0 10 0;
createNode animCurveTA -n "chara_cube_footR_ctrl_rotateX";
	rename -uid "840F80E2-41A0-2094-578C-3A8AEB6290FB";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 4 ".ktv[0:3]"  1 0 5 0 7 0 10 0;
createNode animCurveTA -n "chara_cube_footR_ctrl_rotateY";
	rename -uid "5C5623C1-41F9-3650-DB91-5BB85BBF89E9";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 4 ".ktv[0:3]"  1 -21.355470876880116 5 -25.209687352105174
		 7 0 10 0;
createNode animCurveTA -n "chara_cube_footR_ctrl_rotateZ";
	rename -uid "2D2BD652-46E6-BB23-EA39-15B73B7BF9CE";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 4 ".ktv[0:3]"  1 0 5 0 7 0 10 0;
createNode animCurveTU -n "chara_cube_footR_ctrl_scaleX";
	rename -uid "46BBA48B-4037-6674-5F37-A587C46B79AE";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 4 ".ktv[0:3]"  1 1 5 1 7 1 10 1;
createNode animCurveTU -n "chara_cube_footR_ctrl_scaleY";
	rename -uid "40EEA2A1-47CD-9AD5-B681-D48600427D71";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 4 ".ktv[0:3]"  1 1 5 1 7 1 10 1;
createNode animCurveTU -n "chara_cube_footR_ctrl_scaleZ";
	rename -uid "86FB377D-482F-9D14-149C-3DA68869822D";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 4 ".ktv[0:3]"  1 1 5 1 7 1 10 1;
createNode animCurveTU -n "chara_cube_body_ctrl_squash";
	rename -uid "070806AF-45B6-365C-7616-15B20FF2CE70";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 7 ".ktv[0:6]"  1 0 4 0.30286135922330087 6 0.4084889320388348
		 7 0.46737864077669888 11 -0.085587197115074787 13 0 15 0;
createNode animCurveTU -n "chara_cube_body_ctrl_stretch";
	rename -uid "F9963D92-45B4-DF60-6499-FB8560D3D428";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 7 ".ktv[0:6]"  1 0 4 -0.28637825242718445 6 -0.38625708737864073
		 7 -0.44194174757281557 12 0.19257119350891827 13 0 15 0;
createNode animCurveTU -n "chara_cube_body_ctrl_wiggleFB";
	rename -uid "D9471F7E-48E1-D477-EEA1-D8B64A24F04C";
	setAttr ".tan" 1;
	setAttr ".wgt" no;
	setAttr -s 8 ".ktv[0:7]"  1 0.8 3 0.24827985807089215 4 -0.38650471230681049
		 6 -0.80233009708737868 10 0.099933203883494914 11 0.34233612970119714 14 0.046669808653030387
		 15 0;
	setAttr -s 8 ".kit[3:7]"  18 1 18 18 18;
	setAttr -s 8 ".kot[3:7]"  18 1 18 18 18;
	setAttr -s 8 ".kix[0:7]"  0.18309537751317284 0.070625397693519637 
		0.069882860601351723 1 0.099196525293634222 1 0.36292527213576764 1;
	setAttr -s 8 ".kiy[0:7]"  -0.98309515446537965 -0.99750290886825599 
		-0.9975552043842848 0 0.99506786169068362 0 -0.93181824775284305 0;
	setAttr -s 8 ".kox[0:7]"  0.18309537643546311 0.070625391153181213 
		0.069882871396411472 1 0.099196526514039984 1 0.3629252721357677 1;
	setAttr -s 8 ".koy[0:7]"  -0.98309515466609654 -0.99750290933132624 
		-0.99755520362804617 0 0.9950678615690236 0 -0.93181824775284317 0;
createNode animCurveTU -n "chara_cube_body_ctrl_wiggleLR";
	rename -uid "C08C85FA-48BD-3372-CED1-A48BDE35C9F6";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 9 ".ktv[0:8]"  1 0 3 -0.015695857707965059 4 -0.013243379941095521
		 6 -0.0080659268777042697 8 0 10 0 12 0 13 0 15 0;
createNode animCurveTL -n "chara_cube_body_ctrl_translateX";
	rename -uid "C596DD1F-4033-8BBA-83A7-67ACE6F74D4D";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 9 ".ktv[0:8]"  1 0 3 -0.95924001597690101 4 -0.80935876348051028
		 6 -0.49294278598812979 8 0 10 0 12 0 13 0 15 0;
createNode animCurveTL -n "chara_cube_body_ctrl_translateY";
	rename -uid "22A3100D-47D1-272A-2FD0-60860FD0E856";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 7 ".ktv[0:6]"  1 0 4 -35.84181898691385 6 -42.479192873379375
		 8 -34.611154719526937 10 -4.2988554912674886 13 0.61862902242785367 15 0;
createNode animCurveTL -n "chara_cube_body_ctrl_translateZ";
	rename -uid "53C30247-403B-5B15-3698-1CA37BB4174A";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 7 ".ktv[0:6]"  1 -28.961996475724675 3 12.548799231988589
		 6 -8.0725152117706429 8 -29.872190480887525 10 -7.7446419765263954 13 0.86608063139899372
		 15 0;
	setAttr -s 7 ".kit[0:6]"  1 18 18 18 1 18 18;
	setAttr -s 7 ".kot[0:6]"  1 18 18 18 1 18 18;
	setAttr -s 7 ".kix[0:6]"  0.0008034683748550217 1 0.003928842336610369 
		1 0.0046697012290420961 1 1;
	setAttr -s 7 ".kiy[0:6]"  0.99999967721923322 0 -0.99999228206916391 
		0 0.99998909688577686 0 0;
	setAttr -s 7 ".kox[0:6]"  0.00080346881742817246 1 0.003928842336610369 
		1 0.0046697020273309686 1 1;
	setAttr -s 7 ".koy[0:6]"  0.99999967721887761 0 -0.99999228206916368 
		0 0.99998909688204907 0 0;
createNode animCurveTA -n "chara_cube_body_ctrl_rotateX";
	rename -uid "6915C50D-4140-6E27-C092-49B46A36EA79";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 6 ".ktv[0:5]"  1 4.4856008022134839 4 18.961671088171858
		 6 16.622903076438302 10 -3.5264828708046361 14 0.49490321794228709 15 0;
	setAttr -s 6 ".kit[0:5]"  1 18 18 18 18 18;
	setAttr -s 6 ".kot[0:5]"  1 18 18 18 18 18;
	setAttr -s 6 ".kix[0:5]"  0.24546674887659081 1 0.47814228112223517 
		1 1 1;
	setAttr -s 6 ".kiy[0:5]"  0.96940501091956233 0 -0.87828239137718433 
		0 0 0;
	setAttr -s 6 ".kox[0:5]"  0.24546686892319622 1 0.47814228112223517 
		1 1 1;
	setAttr -s 6 ".koy[0:5]"  0.96940498052209445 0 -0.87828239137718433 
		0 0 0;
createNode animCurveTA -n "chara_cube_body_ctrl_rotateY";
	rename -uid "8907E949-4345-6844-423D-958D7231D734";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 9 ".ktv[0:8]"  1 0 3 0 4 0 6 0 8 0 10 0 12 0 13 0 15 0;
createNode animCurveTA -n "chara_cube_body_ctrl_rotateZ";
	rename -uid "14351F6A-4637-0FE9-510B-239893A9641E";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 9 ".ktv[0:8]"  1 0 3 -0.4074093908504634 4 -0.34375167353007857
		 6 -0.20936315918704373 8 0 10 0 12 0 13 0 15 0;
createNode polyDisc -n "polyDisc1";
	rename -uid "6772298C-4606-E1A4-0A44-AD9AE6A257CF";
createNode lambert -n "lambert2";
	rename -uid "412DD4DA-49E7-5324-E549-1C9D185EA30C";
	setAttr ".c" -type "float3" 0.0221 0.035300002 0.0451 ;
createNode shadingEngine -n "lambert2SG";
	rename -uid "0093A2F6-4D5B-7F06-E245-9FADABDC6362";
	setAttr ".ihi" 0;
	setAttr ".ro" yes;
createNode materialInfo -n "materialInfo1";
	rename -uid "438CE04D-4428-DC9D-B7B1-798E97508137";
createNode animCurveTL -n "chara_cube_main_ctrl_translateX";
	rename -uid "38AE5A81-4E5B-35F1-7BDA-AD85E3F2989F";
	setAttr ".tan" 2;
	setAttr ".wgt" no;
	setAttr -s 2 ".ktv[0:1]"  1 0 10 0;
createNode animCurveTL -n "chara_cube_main_ctrl_translateY";
	rename -uid "5EBA493F-4A09-D38E-8BA6-538E4246B275";
	setAttr ".tan" 2;
	setAttr ".wgt" no;
	setAttr -s 2 ".ktv[0:1]"  1 0 10 0;
createNode animCurveTL -n "chara_cube_main_ctrl_translateZ";
	rename -uid "FFA64F4F-43BA-6634-3CCE-F0ACC7BF6C8E";
	setAttr ".tan" 2;
	setAttr ".wgt" no;
	setAttr -s 2 ".ktv[0:1]"  1 0 10 0;
createNode animCurveTA -n "chara_cube_main_ctrl_rotateX";
	rename -uid "C58611B2-4134-90BA-6DFD-03946F53C8DF";
	setAttr ".tan" 2;
	setAttr ".wgt" no;
	setAttr -s 2 ".ktv[0:1]"  1 0 10 0;
createNode animCurveTA -n "chara_cube_main_ctrl_rotateY";
	rename -uid "F4702975-4BA3-D3C5-8D01-E5B262423A83";
	setAttr ".tan" 2;
	setAttr ".wgt" no;
	setAttr -s 2 ".ktv[0:1]"  1 0 10 0;
createNode animCurveTA -n "chara_cube_main_ctrl_rotateZ";
	rename -uid "E33C396D-4BB3-C596-430B-08BEB291C19D";
	setAttr ".tan" 2;
	setAttr ".wgt" no;
	setAttr -s 2 ".ktv[0:1]"  1 0 10 0;
createNode animCurveTU -n "chara_cube_main_ctrl_scaleX";
	rename -uid "C9A307E4-4B99-5A20-C567-BE80B25CD442";
	setAttr ".tan" 2;
	setAttr ".wgt" no;
	setAttr -s 2 ".ktv[0:1]"  1 1 10 1;
createNode animCurveTU -n "chara_cube_main_ctrl_scaleY";
	rename -uid "C7B15B5C-4302-C940-EE13-92BECA97AEFB";
	setAttr ".tan" 2;
	setAttr ".wgt" no;
	setAttr -s 2 ".ktv[0:1]"  1 1 10 1;
createNode animCurveTU -n "chara_cube_main_ctrl_scaleZ";
	rename -uid "EEA77AA3-463E-7A5E-088E-31B672137E55";
	setAttr ".tan" 2;
	setAttr ".wgt" no;
	setAttr -s 2 ".ktv[0:1]"  1 1 10 1;
createNode animCurveTU -n "chara_cube_footL_ctrl_visibility";
	rename -uid "F029A528-4A36-3ECA-BFD5-ED81398AB75D";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 3 ".ktv[0:2]"  1 1 7 1 10 1;
	setAttr -s 3 ".kit[1:2]"  9 18;
	setAttr -s 3 ".kot[1:2]"  5 18;
createNode animCurveTU -n "chara_cube_footR_ctrl_visibility";
	rename -uid "EC95A82A-4BF8-EF92-A6EE-5396F5DC49D0";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 4 ".ktv[0:3]"  1 1 5 1 7 1 10 1;
	setAttr -s 4 ".kit[1:3]"  9 9 18;
	setAttr -s 4 ".kot[1:3]"  5 5 18;
createNode animCurveTU -n "chara_cube_body_ctrl_visibility";
	rename -uid "64B780CA-4A70-38E6-1171-3B9CE198D44E";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 9 ".ktv[0:8]"  1 1 3 1 4 1 6 1 8 1 10 1 12 1 13 1 15 1;
	setAttr -s 9 ".kit[2:8]"  9 9 18 9 18 9 9;
	setAttr -s 9 ".kot[2:8]"  5 5 18 5 18 5 5;
createNode animCurveTU -n "chara_cube_main_ctrl_visibility";
	rename -uid "11D4EE63-4D96-7656-AE31-3DA172D0E7A5";
	setAttr ".tan" 2;
	setAttr ".wgt" no;
	setAttr ".ktv[0]"  10 1;
createNode animCurveTU -n "chara_cube_body_ctrl_wiggleUD";
	rename -uid "DF1C30CD-4747-9F05-49CE-019A544A7851";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr -s 6 ".ktv[0:5]"  1 -0.10088962019447645 4 0.65935156812759266
		 6 0.72139679927876887 11 -0.22927041586708863 13 0 15 0;
	setAttr -s 6 ".kit[0:5]"  1 18 18 18 18 18;
	setAttr -s 6 ".kot[0:5]"  1 18 18 18 18 18;
	setAttr -s 6 ".kix[0:5]"  0.093660528690155523 0.33718688905489025 
		1 1 1 1;
	setAttr -s 6 ".kiy[0:5]"  0.99560419111486298 0.94143773126505037 
		0 0 0 0;
	setAttr -s 6 ".kox[0:5]"  0.093660522714903827 0.33718688905489025 
		1 1 1 1;
	setAttr -s 6 ".koy[0:5]"  0.99560419167697911 0.94143773126505037 
		0 0 0 0;
select -ne :time1;
	setAttr -av -k on ".cch";
	setAttr -av -cb on ".ihi";
	setAttr -av -k on ".nds";
	setAttr -cb on ".bnm";
	setAttr -k on ".o" 14;
	setAttr -av -k on ".unw" 14;
	setAttr -av -k on ".etw";
	setAttr -av -k on ".tps";
	setAttr -av -k on ".tms";
select -ne :hardwareRenderingGlobals;
	setAttr -av -k on ".ihi";
	setAttr ".otfna" -type "stringArray" 22 "NURBS Curves" "NURBS Surfaces" "Polygons" "Subdiv Surface" "Particles" "Particle Instance" "Fluids" "Strokes" "Image Planes" "UI" "Lights" "Cameras" "Locators" "Joints" "IK Handles" "Deformers" "Motion Trails" "Components" "Hair Systems" "Follicles" "Misc. UI" "Ornaments"  ;
	setAttr ".otfva" -type "Int32Array" 22 0 1 1 1 1 1
		 1 1 1 0 0 0 0 0 0 0 0 0
		 0 0 0 0 ;
	setAttr -k on ".hwi";
	setAttr -av ".ta";
	setAttr -av ".tq";
	setAttr -av ".aoam";
	setAttr -av ".aora";
	setAttr -av ".hfd";
	setAttr -av ".hfe";
	setAttr -av ".hfa";
	setAttr -av ".mbe";
	setAttr -av -k on ".mbsof";
	setAttr -k on ".blen";
	setAttr -k on ".blat";
	setAttr ".msaa" yes;
	setAttr ".fprt" yes;
select -ne :renderPartition;
	setAttr -av -k on ".cch";
	setAttr -cb on ".ihi";
	setAttr -av -k on ".nds";
	setAttr -cb on ".bnm";
	setAttr -s 10 ".st";
	setAttr -cb on ".an";
	setAttr -cb on ".pt";
select -ne :renderGlobalsList1;
	setAttr -k on ".cch";
	setAttr -cb on ".ihi";
	setAttr -k on ".nds";
	setAttr -cb on ".bnm";
select -ne :defaultShaderList1;
	setAttr -k on ".cch";
	setAttr -cb on ".ihi";
	setAttr -k on ".nds";
	setAttr -cb on ".bnm";
	setAttr -s 12 ".s";
select -ne :postProcessList1;
	setAttr -k on ".cch";
	setAttr -cb on ".ihi";
	setAttr -k on ".nds";
	setAttr -cb on ".bnm";
	setAttr -s 2 ".p";
select -ne :defaultRenderingList1;
	setAttr -k on ".ihi";
	setAttr -s 2 ".r";
select -ne :lightList1;
	setAttr -s 3 ".l";
select -ne :initialShadingGroup;
	setAttr -av -k on ".cch";
	setAttr -cb on ".ihi";
	setAttr -av -k on ".nds";
	setAttr -cb on ".bnm";
	setAttr -k on ".mwc";
	setAttr -cb on ".an";
	setAttr -cb on ".il";
	setAttr -cb on ".vo";
	setAttr -cb on ".eo";
	setAttr -cb on ".fo";
	setAttr -cb on ".epo";
	setAttr -k on ".ro" yes;
select -ne :initialParticleSE;
	setAttr -av -k on ".cch";
	setAttr -cb on ".ihi";
	setAttr -av -k on ".nds";
	setAttr -cb on ".bnm";
	setAttr -k on ".mwc";
	setAttr -cb on ".an";
	setAttr -cb on ".il";
	setAttr -cb on ".vo";
	setAttr -cb on ".eo";
	setAttr -cb on ".fo";
	setAttr -cb on ".epo";
	setAttr -k on ".ro" yes;
select -ne :defaultRenderGlobals;
	setAttr -av -k on ".cch";
	setAttr -cb on ".ihi";
	setAttr -av -k on ".nds";
	setAttr -cb on ".bnm";
	setAttr -av -k on ".macc";
	setAttr -av -k on ".macd";
	setAttr -av -k on ".macq";
	setAttr -av -k on ".mcfr";
	setAttr -cb on ".ifg";
	setAttr -av -k on ".clip";
	setAttr -av -k on ".edm";
	setAttr -av -k on ".edl";
	setAttr -k on ".ren" -type "string" "arnold";
	setAttr -av -k on ".esr";
	setAttr -av -k on ".ors";
	setAttr -cb on ".sdf";
	setAttr -av -k on ".outf";
	setAttr -av -cb on ".imfkey";
	setAttr -av -k on ".gama";
	setAttr -av -k on ".an";
	setAttr -k on ".ar";
	setAttr -k on ".fs" 1;
	setAttr -k on ".ef" 10;
	setAttr -av -k on ".bfs";
	setAttr -cb on ".me";
	setAttr -cb on ".se";
	setAttr -av -k on ".be";
	setAttr -av -cb on ".ep";
	setAttr -av -k on ".fec";
	setAttr -av -k on ".ofc";
	setAttr -cb on ".ofe";
	setAttr -cb on ".efe";
	setAttr -cb on ".oft";
	setAttr -cb on ".umfn";
	setAttr -cb on ".ufe";
	setAttr -av -k on ".pff";
	setAttr -av -k on ".peie";
	setAttr -av -cb on ".ifp";
	setAttr -k on ".rv";
	setAttr -av -k on ".comp";
	setAttr -av -k on ".cth";
	setAttr -av -k on ".soll";
	setAttr -cb on ".sosl";
	setAttr -av -k on ".rd";
	setAttr -av -k on ".lp";
	setAttr -av -k on ".sp";
	setAttr -av -k on ".shs";
	setAttr -av -k on ".lpr";
	setAttr -cb on ".gv";
	setAttr -cb on ".sv";
	setAttr -av -k on ".mm";
	setAttr -av -k on ".npu";
	setAttr -av -k on ".itf";
	setAttr -av -k on ".shp";
	setAttr -cb on ".isp";
	setAttr -av -k on ".uf";
	setAttr -av -k on ".oi";
	setAttr -av -k on ".rut";
	setAttr -av -k on ".mot";
	setAttr -av -k on ".mb";
	setAttr -av -k on ".mbf";
	setAttr -av -k on ".mbso";
	setAttr -av -k on ".mbsc";
	setAttr -av -k on ".afp";
	setAttr -av -k on ".pfb";
	setAttr -k on ".pram";
	setAttr -k on ".poam";
	setAttr -k on ".prlm";
	setAttr -k on ".polm";
	setAttr -cb on ".prm";
	setAttr -cb on ".pom";
	setAttr -cb on ".pfrm";
	setAttr -cb on ".pfom";
	setAttr -av -k on ".bll";
	setAttr -av -k on ".bls";
	setAttr -av -k on ".smv";
	setAttr -av -k on ".ubc";
	setAttr -av -k on ".mbc";
	setAttr -cb on ".mbt";
	setAttr -av -k on ".udbx";
	setAttr -av -k on ".smc";
	setAttr -av -k on ".kmv";
	setAttr -cb on ".isl";
	setAttr -cb on ".ism";
	setAttr -cb on ".imb";
	setAttr -av -k on ".rlen";
	setAttr -av -k on ".frts";
	setAttr -av -k on ".tlwd";
	setAttr -av -k on ".tlht";
	setAttr -av -k on ".jfc";
	setAttr -cb on ".rsb";
	setAttr -av -k on ".ope";
	setAttr -av -k on ".oppf";
	setAttr -av -k on ".rcp";
	setAttr -av -k on ".icp";
	setAttr -av -k on ".ocp";
	setAttr -cb on ".hbl";
select -ne :defaultResolution;
	setAttr -av -k on ".cch";
	setAttr -av -k on ".ihi";
	setAttr -av -k on ".nds";
	setAttr -k on ".bnm";
	setAttr -av -k on ".w";
	setAttr -av -k on ".h";
	setAttr -av -k on ".pa" 1;
	setAttr -av -k on ".al";
	setAttr -av -k on ".dar";
	setAttr -av -k on ".ldar";
	setAttr -av -k on ".dpi";
	setAttr -av -k on ".off";
	setAttr -av -k on ".fld";
	setAttr -av -k on ".zsl";
	setAttr -av -k on ".isu";
	setAttr -av -k on ".pdu";
select -ne :defaultLightSet;
	setAttr -k on ".cch";
	setAttr -k on ".ihi";
	setAttr -av -k on ".nds";
	setAttr -k on ".bnm";
	setAttr -s 3 ".dsm";
	setAttr -k on ".mwc";
	setAttr -k on ".an";
	setAttr -k on ".il";
	setAttr -k on ".vo";
	setAttr -k on ".eo";
	setAttr -k on ".fo";
	setAttr -k on ".epo";
	setAttr -k on ".ro";
select -ne :hardwareRenderGlobals;
	setAttr -k on ".cch";
	setAttr -cb on ".ihi";
	setAttr -k on ".nds";
	setAttr -cb on ".bnm";
	setAttr -k off -cb on ".ctrs" 256;
	setAttr -av -k off -cb on ".btrs" 512;
	setAttr -k off -cb on ".fbfm";
	setAttr -k off -cb on ".ehql";
	setAttr -k off -cb on ".eams";
	setAttr -k off -cb on ".eeaa";
	setAttr -k off -cb on ".engm";
	setAttr -k off -cb on ".mes";
	setAttr -k off -cb on ".emb";
	setAttr -av -k off -cb on ".mbbf";
	setAttr -k off -cb on ".mbs";
	setAttr -k off -cb on ".trm";
	setAttr -k off -cb on ".tshc";
	setAttr -k off -cb on ".enpt";
	setAttr -k off -cb on ".clmt";
	setAttr -k off -cb on ".tcov";
	setAttr -k off -cb on ".lith";
	setAttr -k off -cb on ".sobc";
	setAttr -k off -cb on ".cuth";
	setAttr -k off -cb on ".hgcd";
	setAttr -k off -cb on ".hgci";
	setAttr -k off -cb on ".mgcs";
	setAttr -k off -cb on ".twa";
	setAttr -k off -cb on ".twz";
	setAttr -k on ".hwcc";
	setAttr -k on ".hwdp";
	setAttr -k on ".hwql";
	setAttr -k on ".hwfr";
	setAttr -k on ".soll";
	setAttr -k on ".sosl";
	setAttr -k on ".bswa";
	setAttr -k on ".shml";
	setAttr -k on ".hwel";
connectAttr "chara_cube_main_ctrl_translateX.o" "box_character_rig_01RN.phl[1]";
connectAttr "chara_cube_main_ctrl_translateY.o" "box_character_rig_01RN.phl[2]";
connectAttr "chara_cube_main_ctrl_translateZ.o" "box_character_rig_01RN.phl[3]";
connectAttr "chara_cube_main_ctrl_visibility.o" "box_character_rig_01RN.phl[4]";
connectAttr "chara_cube_main_ctrl_rotateX.o" "box_character_rig_01RN.phl[5]";
connectAttr "chara_cube_main_ctrl_rotateY.o" "box_character_rig_01RN.phl[6]";
connectAttr "chara_cube_main_ctrl_rotateZ.o" "box_character_rig_01RN.phl[7]";
connectAttr "chara_cube_main_ctrl_scaleX.o" "box_character_rig_01RN.phl[8]";
connectAttr "chara_cube_main_ctrl_scaleY.o" "box_character_rig_01RN.phl[9]";
connectAttr "chara_cube_main_ctrl_scaleZ.o" "box_character_rig_01RN.phl[10]";
connectAttr "chara_cube_footR_ctrl_visibility.o" "box_character_rig_01RN.phl[11]"
		;
connectAttr "chara_cube_footR_ctrl_translateX.o" "box_character_rig_01RN.phl[12]"
		;
connectAttr "chara_cube_footR_ctrl_translateY.o" "box_character_rig_01RN.phl[13]"
		;
connectAttr "chara_cube_footR_ctrl_translateZ.o" "box_character_rig_01RN.phl[14]"
		;
connectAttr "chara_cube_footR_ctrl_rotateX.o" "box_character_rig_01RN.phl[15]";
connectAttr "chara_cube_footR_ctrl_rotateY.o" "box_character_rig_01RN.phl[16]";
connectAttr "chara_cube_footR_ctrl_rotateZ.o" "box_character_rig_01RN.phl[17]";
connectAttr "chara_cube_footR_ctrl_scaleX.o" "box_character_rig_01RN.phl[18]";
connectAttr "chara_cube_footR_ctrl_scaleY.o" "box_character_rig_01RN.phl[19]";
connectAttr "chara_cube_footR_ctrl_scaleZ.o" "box_character_rig_01RN.phl[20]";
connectAttr "chara_cube_footL_ctrl_visibility.o" "box_character_rig_01RN.phl[21]"
		;
connectAttr "chara_cube_footL_ctrl_translateX.o" "box_character_rig_01RN.phl[22]"
		;
connectAttr "chara_cube_footL_ctrl_translateY.o" "box_character_rig_01RN.phl[23]"
		;
connectAttr "chara_cube_footL_ctrl_translateZ.o" "box_character_rig_01RN.phl[24]"
		;
connectAttr "chara_cube_footL_ctrl_rotateX.o" "box_character_rig_01RN.phl[25]";
connectAttr "chara_cube_footL_ctrl_rotateY.o" "box_character_rig_01RN.phl[26]";
connectAttr "chara_cube_footL_ctrl_rotateZ.o" "box_character_rig_01RN.phl[27]";
connectAttr "chara_cube_footL_ctrl_scaleX.o" "box_character_rig_01RN.phl[28]";
connectAttr "chara_cube_footL_ctrl_scaleY.o" "box_character_rig_01RN.phl[29]";
connectAttr "chara_cube_footL_ctrl_scaleZ.o" "box_character_rig_01RN.phl[30]";
connectAttr "chara_cube_body_ctrl_squash.o" "box_character_rig_01RN.phl[31]";
connectAttr "chara_cube_body_ctrl_stretch.o" "box_character_rig_01RN.phl[32]";
connectAttr "chara_cube_body_ctrl_wiggleFB.o" "box_character_rig_01RN.phl[33]";
connectAttr "chara_cube_body_ctrl_wiggleLR.o" "box_character_rig_01RN.phl[34]";
connectAttr "chara_cube_body_ctrl_wiggleUD.o" "box_character_rig_01RN.phl[35]";
connectAttr "chara_cube_body_ctrl_visibility.o" "box_character_rig_01RN.phl[36]"
		;
connectAttr "chara_cube_body_ctrl_translateX.o" "box_character_rig_01RN.phl[37]"
		;
connectAttr "chara_cube_body_ctrl_translateY.o" "box_character_rig_01RN.phl[38]"
		;
connectAttr "chara_cube_body_ctrl_translateZ.o" "box_character_rig_01RN.phl[39]"
		;
connectAttr "chara_cube_body_ctrl_rotateX.o" "box_character_rig_01RN.phl[40]";
connectAttr "chara_cube_body_ctrl_rotateY.o" "box_character_rig_01RN.phl[41]";
connectAttr "chara_cube_body_ctrl_rotateZ.o" "box_character_rig_01RN.phl[42]";
connectAttr "polyDisc1.output" "pDiscShape1.i";
relationship "link" ":lightLinker1" ":initialShadingGroup.message" ":defaultLightSet.message";
relationship "link" ":lightLinker1" ":initialParticleSE.message" ":defaultLightSet.message";
relationship "link" ":lightLinker1" "lambert2SG.message" ":defaultLightSet.message";
relationship "shadowLink" ":lightLinker1" ":initialShadingGroup.message" ":defaultLightSet.message";
relationship "shadowLink" ":lightLinker1" ":initialParticleSE.message" ":defaultLightSet.message";
relationship "shadowLink" ":lightLinker1" "lambert2SG.message" ":defaultLightSet.message";
connectAttr "layerManager.dli[0]" "defaultLayer.id";
connectAttr "renderLayerManager.rlmi[0]" "defaultRenderLayer.rlid";
connectAttr "lambert2.oc" "lambert2SG.ss";
connectAttr "pDiscShape1.iog" "lambert2SG.dsm" -na;
connectAttr "lambert2SG.msg" "materialInfo1.sg";
connectAttr "lambert2.msg" "materialInfo1.m";
connectAttr "lambert2SG.pa" ":renderPartition.st" -na;
connectAttr "lambert2.msg" ":defaultShaderList1.s" -na;
connectAttr "defaultRenderLayer.msg" ":defaultRenderingList1.r" -na;
connectAttr "directionalLightShape1.ltd" ":lightList1.l" -na;
connectAttr "directionalLightShape2.ltd" ":lightList1.l" -na;
connectAttr "directionalLightShape3q.ltd" ":lightList1.l" -na;
connectAttr "directionalLight1.iog" ":defaultLightSet.dsm" -na;
connectAttr "directionalLight2.iog" ":defaultLightSet.dsm" -na;
connectAttr "directionalLight3.iog" ":defaultLightSet.dsm" -na;
// End of box_character_clip_stop_01.ma
