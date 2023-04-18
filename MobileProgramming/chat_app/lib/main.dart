import 'package:chat_app/firebase_options.dart';
import 'package:firebase_auth/firebase_auth.dart';
import 'package:firebase_core/firebase_core.dart';
import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

import 'chat_service.dart';
import 'screens/channels_screen.dart';
import 'screens/login_screen.dart';
import 'models/user.dart';

void main() async {
  WidgetsFlutterBinding.ensureInitialized();
  await Firebase.initializeApp(options: DefaultFirebaseOptions.currentPlatform);
  runApp(const MyApp());
}

class MyApp extends StatelessWidget {
  const MyApp({super.key});

  @override
  Widget build(BuildContext context) {
    return MultiProvider(
      providers: [
        Provider(create: (context) => ChatService()),
        StreamProvider(create: (context) => FirebaseAuth.instance.authStateChanges(), initialData: null),
      ],
      builder: (context, child) {
        final user = Provider.of<User?>(context);
        return MaterialApp(
          title: 'Chat',
          theme: ThemeData(useMaterial3: true),
          home: user == null ? const LoginScreen() : const ChannelsScreen(),
        );
      },
    );
  }
}
