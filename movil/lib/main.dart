import 'package:flutter/material.dart';
import 'package:flutter_localizations/flutter_localizations.dart';

import 'pages/home_page.dart';
import 'pages/registro_correspondencia_page.dart';
import 'services/correspondencia_api.dart';

void main() => runApp(const CorrespondenciaApp());

class CorrespondenciaApp extends StatelessWidget {
  const CorrespondenciaApp({super.key});
  @override
  Widget build(BuildContext context) => MaterialApp(
    title: 'Sistema de Correspondencia',
    debugShowCheckedModeBanner: false,
    locale: const Locale('es'),
    supportedLocales: const [Locale('es')],
    localizationsDelegates: GlobalMaterialLocalizations.delegates,
    theme: ThemeData(
      useMaterial3: true,
      colorScheme: ColorScheme.fromSeed(seedColor: const Color(0xFF8BC34A))
          .copyWith(
            primary: const Color(0xFF456B2F),
            onPrimary: Colors.white,
            surface: Colors.white,
            onSurface: const Color(0xFF202020),
          ),
      scaffoldBackgroundColor: Colors.white,
      appBarTheme: const AppBarTheme(
        backgroundColor: Color(0xFF456B2F),
        foregroundColor: Colors.white,
        elevation: 0,
      ),
      inputDecorationTheme: InputDecorationTheme(
        filled: true,
        fillColor: const Color(0xFFF4F5F2),
        border: OutlineInputBorder(borderRadius: BorderRadius.circular(8)),
        contentPadding: const EdgeInsets.symmetric(
          horizontal: 16,
          vertical: 16,
        ),
      ),
      filledButtonTheme: FilledButtonThemeData(
        style: FilledButton.styleFrom(
          backgroundColor: const Color(0xFF8BC34A),
          foregroundColor: const Color(0xFF202020),
          minimumSize: const Size(0, 52),
          shape: RoundedRectangleBorder(borderRadius: BorderRadius.circular(8)),
        ),
      ),
    ),
    home: const PantallaPrincipal(),
  );
}

class PantallaPrincipal extends StatefulWidget {
  const PantallaPrincipal({super.key});
  @override
  State<PantallaPrincipal> createState() => _PantallaPrincipalState();
}

class _PantallaPrincipalState extends State<PantallaPrincipal> {
  final _api = CorrespondenciaApi();
  int _pagina = 0;
  @override
  void dispose() {
    _api.close();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) => Scaffold(
    appBar: AppBar(
      title: const Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          Text('Sistema de Correspondencia', style: TextStyle(fontSize: 17)),
          Text(
            'Concejo Municipal de Santa Cruz',
            style: TextStyle(fontSize: 12),
          ),
        ],
      ),
    ),
    drawer: Drawer(
      child: SafeArea(
        child: ListView(
          children: [
            const Padding(
              padding: EdgeInsets.fromLTRB(28, 32, 16, 24),
              child: Text('Correspondencia', style: TextStyle(fontSize: 20)),
            ),
            ListTile(
              leading: const Icon(Icons.home_outlined),
              title: const Text('Inicio'),
              selected: _pagina == 0,
              onTap: () {
                Navigator.pop(context);
                setState(() => _pagina = 0);
              },
            ),
            ListTile(
              leading: const Icon(Icons.note_add_outlined),
              title: const Text('Nueva correspondencia'),
              selected: _pagina == 1,
              onTap: () {
                Navigator.pop(context);
                setState(() => _pagina = 1);
              },
            ),
          ],
        ),
      ),
    ),
    body: SafeArea(
      child: IndexedStack(
        index: _pagina,
        children: [
          HomePage(onNuevoRegistro: () => setState(() => _pagina = 1)),
          RegistroCorrespondenciaPage(api: _api),
        ],
      ),
    ),
  );
}
