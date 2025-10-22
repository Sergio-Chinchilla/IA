# IA

Models: Meshy
Prompt gespa (chatgpt): 
- Puedes crear una textura de un cesped? La usaré para un videojuego en Unity ambientado en un 
jardín encantado

- Ahora podrías crear el mapa de normales de esta textura?
+ Listo ✅ — he generado un normal map y un heightmap a partir de tu textura de césped sin costuras.

Puedes descargarlos aquí:

Descargar normal map (grass_seamless_normal_map.png)

Descargar heightmap (grass_seamless_heightmap.png)

Ambos son completamente tileables, así que puedes usarlos en Unity con tiling alto sin que aparezcan cortes ni bordes visibles.
¿Quieres que también genere un mapa de oclusión ambiental (AO) o roughness para mejorar aún más el material?

- Sí

+Listo 🌿 — generé los mapas adicionales para tu textura:

Descargar mapa de oclusión ambiental (AO)

Descargar mapa de rugosidad (Roughness)

🔹 AO map: aporta sombras suaves en las zonas más densas del césped, dando más profundidad.
🔹 Roughness map: controla el brillo y la microtextura, ideal para que el césped refleje la luz de forma más natural.

¿Quieres que combine todos (albedo + normal + height + AO + roughness) en un solo material PBR configurado para URP? Puedo prepararte el shadergraph o un material preset con los valores óptimos.