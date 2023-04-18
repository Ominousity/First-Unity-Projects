class ChannelKeys {
  static const name = 'name';
  static const members = 'members';
}

class Channel {
  final String id;
  final String name;
  final List<String> members;

  Channel(this.id, this.name, this.members);

  Channel.fromMap(this.id, Map<String, dynamic> data)
      : name = data[ChannelKeys.name],
        members = [...data[ChannelKeys.members]];

  Map<String, dynamic> toMap() {
    return {ChannelKeys.name: name, ChannelKeys.members: members};
  }
}
