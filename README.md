## Interfaces Inteligentes; Práctica Reconocimiento de Voz

Marcos Jesús Barrios Lorenzo

alu0101056944

![Demo](assets/demo.gif)

# Explicación del código

Hay un spawner de objetos. El objetivo es eliminar todos los objetos que han sido spawneados. Para ello, se puede ir mencionando el nombre de cada objeto, si se utiliza el modo palabras clave ó se pueden eliminar en el modo dictado, donde hay que pronunciar "eliminar" seguido de los nombres de los objetos. Los scripts de reconocimiento de voz (<code>MyKeywordRecognizer</code>, <code>MyDictationRecognizer</code>) contienen la lógica de eliminación de los objetos a partir de la voz. SpawnGameObjectsWithinBounds contiene el método spawnAndDestroy() invocado por los scripts de botón (<code>ButtonStartDictationGame</code>, <code>ButtonStartKeywordGame</code>), que también llaman al reconocedor de voz correspondiente. Para activar los botones se utilizó programación por eventos. El script <code>PlayerRaycast</code> contiene un evento que se activa cuando el Raycast colisiona con cualquier objeto. Los botones están suscritos de forma que permiten ser clickeados cuando el jugador los mira, es decir, cuando el Raycast del jugador colisiona con el botón.

[SpawnGameObjectsWithinBounds](src/SpawnGameObjectWithinBounds.cs) Está en un GameObject propio con un Collider en modo trigger para poder spawnear los objetos a ser eliminados durante el juego. Consiste en un array de objetos a ser spawneados y otro de los que ya han sido spawneados, que tendrán (Clone) en el nombre. Para cada objeto a spawnear destruyo el instanciado (su clon) si ya ha sido instanciado previamente, calculo una nueva posición dentro del área del collider y lo instancio de nuevo.

[PlayerRayCast](src/PlayerRayCast.cs) Componente de un objeto RaycastOrigin hijo de la Cámara del controlador en primera persona. Obtiene un rayo que va en la dirección del centro de la pantalla desde la cámara y comprueba si colisiona con algo. Si colisiona llama a un evento al que el resto de objetos se pueden suscribir para reaccionar, como los botones en el caso de este proyecto.

[MyKeywordRecognizer](src/MyKeywordRecognizer.cs) Componente del pedestal para elegir jugar en modo palabras claves. Obtiene un array con todos los GameObject etiquetados como spawnable y obtiene otro array con sus nombres, que son los que habrá que pronunciar para eliminarlos. Instancia el KeywordRecognizer, le pasa los nombres como palabras clave a escuchar, en este caso "pelota", "libro" y "taburete", y cada vez que escucha una palabra comprueba si pertenece al nombre de un spawnable antes de eliminarlo.

[MyDictationRecognizer](src/MyDictationRecognizer.cs) Componente del pedestal para elegir jugar en modo dictado. Para eliminar un objeto spawneado debes pronunciar "eliminar" seguido de cualquier frase que contenga los nombres de los objetos etiquetados como Spawnable. Obtienes un array con los GameObject con tag Spawnable, registra callbacks para logear cualquier problema y la hipótesis en cada momento. Cuando reconoce una frase con éxito, si la primera palabra es "eliminar" se comprueba el resto de palabras y las que coinciden con el nombre de un GameObject Spawnable eliminan el primer objeto que encuentre en la escena con ese nombre.

[ButtonStartDictationGame](src/ButtonStartDictationGame.cs) Se suscribe al evento para saber a donde mira el jugador. Si éste está suficientemente cerca y clickea mirando al botón llama al script de <code>MyKeywordRecognizer</code> para activar el reconocimiento de palabras clave y spawnea los GameObject de tag <code>Spawnable</code> a ser eliminados.

[ButtonStartKeywordGame](src/ButtonStartKeywordGame.cs) Hace lo mismo que [ButtonStartDictationGame](src/ButtonStartDictationGame.cs) pero utiliza reconocimiento de palabras claves en vez de dictado.


# Trabajo realizado

Se crearon cinco planos que forman el interior de un edificio; cuatro para paredes y un último para el suelo. Después se descargó el controlador en primera persona para navegar el interior. Una sala adyancente contendrá los objetos cuyo nombre debe pronunciar el jugador para ganar el juego de las keywords. Un controlador en primera persona fue importado a la escena.

Se creó un pedestal con un modelo de micrófono en 3D al frente de una ventana de cristal que separa la sala de juego de la de comienzo. A continuación, se agregó un gameobject vacío con un box collider que representa el área donde pueden spawnear los objetos a ser nombrados en el juego. Se le puso la etiqueta <code>Spawner</code>. Un script[SpawnGameObjectsWithinBounds](src/SpawnGameObjectWithinBounds.cs) se encarga de spawnear los gameobjects a ser eliminados a través de reconocimiento de voz. Se descargó un modelo de pelota de futbol y de taburete para agregarlos al script del spawner como objetos a spawnear. También un modelo de libro.

Se introdujo un modelo 3D de caja a modo de botón en el pedestal. También una etiqueta en 3D que señala el botón como comienzo del juego.

Se creó la etiqueta <code>Spawnable</code> de forma que cuando se quiera agregar un nuevo objeto solo haya que insertarlo en la escena con esa etiqueta. El nombre del objeto será la keyword a pronunciar para su eliminación tras ser spawneado.

Después se implementó el script de dictado [MyDictationRecognizer](src/MyDictationRecognizer.cs) para que al dictar la frase "eliminar ..." se lo considere un comando a partir del cual cualquier palabra será comparada con el nombre de un objeto a eliminar y será eliminado si coincide. Por ejemplo: tras haber spawneado una silla y una pelota, dictar la frase "eliminar una silla y una pelota" eliminará ambos objetos. Solo se elimina un objeto cada vez, por lo que si hay más de una silla no se eliminarán todas de golpe.

A continuación se agregó un objeto vacío, <code>RaycastOrigin</code>, hijo de la cámara del controlador del jugador, que sirve de origen para utilizar un <code>Physics.Raycast()</code> para saber qué objeto está mirando el jugador. Se implementó un script que traza un rayo en la dirección del centro de la pantalla según la cámara y que llama a un evento para avisar de que ha colisionado con algo. Un segundo script <code>ButtonStartKeywordsGame</code>, agregado al botón del pedestal, se suscribe al evento de colisión del raycast para saber cuando el jugador está mirando al botón. Solo se permite utilizar el botón si el jugador está cerca y lo está mirando, en cuyo caso puede dar un click izquierdo del ratón para accionarlo. El script también activa el reconocimiento de keywords y spawnea los objetos correspondientes en la sala de juego. En otra palabras; comienza el juego utilizando el <code>KeywordRecognizer</code>.

Se creó un segundo pedestal al lado del anterior y se modificó la etiqueta para señalar que se trata del pedestal para comenzar el juego por <code>DictationRecognizer</code>. Se etiquetaron ambos pedestales con Keyword ó Dictation para diferenciarlos. Se agregó el script <code>ButtonStartDictationGame</code>, que igual que el anterior, además de comenzar el juego se asegura de que el otro recognizer está apagado, pues no pueden estar los dos encendidos. Se crearon y asignaron las etiquetas <code>DictationRecognizer</code> y <code>KeywordRecognizer</code> a los pedestales correspondientes. El objetivo es que los scripts de los botones puedan acceder al del recognizer correspondiente.


