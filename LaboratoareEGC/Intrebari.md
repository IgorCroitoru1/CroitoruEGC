1. Ce este un viewport?
Un viewport se referă la o regiune rectangulară a suprafeței de afișare (cum ar fi ecranul unui calculator sau o fereastră de vizualizare) în care
sunt desenate obiectele grafice.

2. Ce reprezintă conceptul de frames per seconds din punctul de vedere al bibliotecii OpenGL?
FPS (Frames Per Second) în OpenGL reprezintă numărul de cadre pe secundă desenate și afișate pe ecran. 
Cu cât acest număr este mai mare, cu atât animațiile par mai fluide.

3. Ce este modul imediat de randare?
Modul imediat de randare (Immediate Mode Rendering) este o abordare în OpenGL în care desenarea se face printr-un set de apeluri directe la funcții OpenGL pentru fiecare vertex și poligon.
Este considerat învechit și ineficient și nu este recomandat în versiunile mai noi de OpenGL.

4. Care este ultima versiune de OpenGL care acceptă modul imediat?
Ultima versiune de OpenGL care acceptă modul imediat este OpenGL 3.0.

5. Când este rulată metoda OnRenderFrame()?
Metoda OnRenderFrame() este rulată în cadrul buclei principale de randare și este utilizată pentru desenarea graficii pe ecran.
Aici, se efectuează toate operațiunile de desenare a obiectelor.

6. De ce este nevoie ca metoda OnResize() să fie executată cel puțin o dată?
Metoda OnResize() trebuie să fie executată cel puțin o dată pentru a inițializa viewport-ul și pentru a se asigura că obiectele desenate vor avea coordonatele corecte în contextul noii dimensiuni a ferestrei.

7. Ce reprezintă parametrii metodei CreatePerspectiveFieldOfView() și care este domeniul de valori pentru aceștia?
Metoda CreatePerspectiveFieldOfView() este folosită pentru a crea o matrice de proiecție perspectivă în OpenGL. 
Parametrii acestei metode reprezintă unghiul de câmp de vedere vertical (fovy), raportul de aspect (aspect ratio), distanța la planul de apropiere (zNear), și distanța la planul de depărtare (zFar).
Valorile pentru acești parametri variază în funcție de cerințele aplicației, dar în general, fovy este un unghi în radiani, aspect este raportul dintre lățimea și înălțimea ferestrei, iar zNear și zFar reprezintă distanța la planurile de apropiere și depărtare.
