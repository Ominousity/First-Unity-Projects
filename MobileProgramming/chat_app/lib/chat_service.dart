import 'dart:async';
import 'dart:math';

import 'package:cloud_firestore/cloud_firestore.dart';

import 'models/channel.dart';
import 'models/message.dart';
import 'models/sender.dart';
import 'models/user.dart';

String generateId() {
  return Random().nextInt(2 ^ 53).toString();
}

class CollectionNames{
  static const channels = 'channels';
  static const members = 'members';
  static const name = 'name';
  static const messages = 'messages';
}

class ChatService {
  final List<Channel> _channels = [];
  final Map<String, List<Message>> _messages = {};
  final _channelsController = StreamController<List<Channel>>.broadcast();
  final _messagesController =
      StreamController<Map<String, List<Message>>>.broadcast();

  ChatService() {
    _channelsController.add(_channels);
    _messagesController.add(_messages);
  }

  Stream<Iterable<Channel>> channels(User user) {
    return FirebaseFirestore.instance
    .collection(CollectionNames.channels)
    .where(CollectionNames.members, arrayContains: user.uid)
    .withConverter(fromFirestore: (snapshot, options) => Channel.fromMap(snapshot.id, snapshot.data()!), toFirestore: (value, options) => value.toMap())
    .snapshots()
    .map((querySnapshot) => querySnapshot.docs.map((e) => e.data()));
  }

  Stream<List<Message>> messages(Channel channel) {
    return FirebaseFirestore.instance
    .collection(CollectionNames.channels)
    .doc(channel.id)
    .collection(CollectionNames.messages)
    .orderBy(MessageKeys.timestamp)
    .withConverter(fromFirestore: (snapshot, options) => Message.fromMap(snapshot.id, snapshot.data()!), toFirestore: (value, options) => value.toMap())
    .snapshots()
    .map((event) => event.docs.map((e) => e.data()).toList());
  }

  Future<void> sendMessage(User user, Channel channel, String message) async {
    final sender = Sender(
        uid: user.uid,
        displayName: user.displayName ?? '',
        email: user.email ?? 'Unknown');
    await FirebaseFirestore.instance
    .collection(CollectionNames.channels)
    .doc(channel.id)
    .collection(CollectionNames.messages)
    .add({
      MessageKeys.timestamp: FieldValue.serverTimestamp(),
      MessageKeys.from: sender.toMap(),
      MessageKeys.content: message
    });
  }

  Future<void> createChannel(User user, String name) async {
    await FirebaseFirestore.instance.collection(CollectionNames.channels).add({
      CollectionNames.members: [user.uid],
      CollectionNames.name: name
    });
  }

  void addMember(Channel channel, String uid) {
    channel.members.add(uid);
    return _channelsController.add([..._channels]);
  }
}
